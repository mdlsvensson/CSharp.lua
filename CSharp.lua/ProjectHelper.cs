using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Incubator.Project;

namespace CSharpLua {
  public static class ProjectHelper {
    private static readonly ICakeEnvironment _environment;
    private static readonly IFileSystem _fileSystem;

    static ProjectHelper() {
      var platform = new CakePlatform();
      var runtime = new CakeRuntime();
      var cakeConsole = new CakeConsole();
      var cakeLog = new CakeBuildLog(cakeConsole, Verbosity.Quiet);

      _environment = new CakeEnvironment(platform, runtime, cakeLog);
      _fileSystem = new FileSystem();
    }

    public static CustomProjectParserResult ParseProject(string path, string configuration, string platform = "AnyCPU") {
      return ParseProject(new FilePath(path), Environment.CurrentDirectory, configuration, platform);
    }

    private static CustomProjectParserResult ParseProject(FilePath path, string workingDirectory, string configuration, string platform) {
      if (path.IsRelative) {
        _environment.WorkingDirectory = workingDirectory;
        path = path.MakeAbsolute(_environment);
      }

      return _fileSystem.GetFile(path).ParseProjectFile(configuration, platform);
    }

    public static IEnumerable<(string folder, CustomProjectParserResult project)> EnumerateProjects(this CustomProjectParserResult mainProject) {
      var result = new Dictionary<string, CustomProjectParserResult>();
      void AddReferences(string folderPath, CustomProjectParserResult project) {
        var notCompiled = GetReferencesNotCompiledAsSource(project);
        foreach (var referencedProject in project.ProjectReferences.Select(p => ParseProject(p.FilePath, folderPath, project.Configuration, project.Platform))) {
          if (notCompiled.Contains(System.IO.Path.GetFullPath(referencedProject.ProjectFilePath.FullPath))) {
            continue;
          }

          var directory = referencedProject.GetDirectory();
          if (result.TryAdd(directory, referencedProject)) {
            AddReferences(directory, referencedProject);
          } else if (!string.Equals(referencedProject.ProjectFilePath.FullPath, result[directory].ProjectFilePath.FullPath, StringComparison.OrdinalIgnoreCase)) {
            throw new Exception("Found two or more projects in the same directory.");
          }
        }
      }
      var mainDirectory = mainProject.GetDirectory();
      result.Add(mainDirectory, mainProject);
      AddReferences(mainDirectory, mainProject);
      foreach (var pair in result) {
        yield return (pair.Key, pair.Value);
      }
    }

    // A project reference that is an analyzer (OutputItemType="Analyzer") or has no output assembly (ReferenceOutputAssembly="false")
    // brings no code into the one that references it, so its sources are not compiled with it.
    private static HashSet<string> GetReferencesNotCompiledAsSource(CustomProjectParserResult project) {
      var references = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
      var projectFile = project.ProjectFilePath.FullPath;
      var document = XDocument.Load(projectFile);
      string Metadata(XElement reference, string name) => reference.Attribute(name)?.Value ?? reference.Element(name)?.Value;
      foreach (var reference in document.Descendants().Where(i => i.Name.LocalName == "ProjectReference")) {
        var include = reference.Attribute("Include")?.Value;
        if (string.IsNullOrEmpty(include) || include.Contains("$(")) {
          continue;
        }

        bool isAnalyzer = string.Equals(Metadata(reference, "OutputItemType"), "Analyzer", StringComparison.OrdinalIgnoreCase);
        bool hasNoAssembly = string.Equals(Metadata(reference, "ReferenceOutputAssembly"), "false", StringComparison.OrdinalIgnoreCase);
        if (isAnalyzer || hasNoAssembly) {
          references.Add(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(projectFile), include.Replace('\\', System.IO.Path.DirectorySeparatorChar))));
        }
      }
      return references;
    }

    public static string GetDirectory(this CustomProjectParserResult project) {
      return project.ProjectFilePath.GetDirectory().FullPath;
    }

    public static IEnumerable<string> EnumerateSourceFiles(this CustomProjectParserResult project, string folder = null) {
      folder ??= project.GetDirectory();
      const string ObjFolder = "obj";
      const string OutputFolder = "bin";
      const string SearchPattern = "*.cs";
      foreach (var file in System.IO.Directory.EnumerateFiles(folder, SearchPattern, System.IO.SearchOption.TopDirectoryOnly)) {
        yield return file;
      }
      var ignoredSubFolders = new HashSet<string>() { ObjFolder, OutputFolder };
      foreach (var subFolder in System.IO.Directory.EnumerateDirectories(folder)) {
        var subFolderName = new System.IO.DirectoryInfo(subFolder).Name;
        if (ignoredSubFolders.Contains(subFolderName)) {
          continue;
        }
        foreach (var file in System.IO.Directory.EnumerateFiles(subFolder, SearchPattern, System.IO.SearchOption.AllDirectories)) {
          yield return file;
        }
      }
    }
  }
}
