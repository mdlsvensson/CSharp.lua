using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  [TestClass]
  public class ProjectReferenceTests {
    private static string[] CompiledProjects() {
      var main = Path.GetFullPath(@"..\..\..\Input\ProjectReferences\Main\Main.csproj");
      var project = ProjectHelper.ParseProject(main, "Release");
      return project.EnumerateProjects().Select(i => new DirectoryInfo(i.folder).Name).OrderBy(i => i).ToArray();
    }

    [TestMethod]
    public void TestAProjectReferenceIsCompiledFromSource() {
      CollectionAssert.Contains(CompiledProjects(), "Lib");
    }

    [TestMethod]
    public void TestAnAnalyzerReferenceIsNotCompiled() {
      CollectionAssert.DoesNotContain(CompiledProjects(), "Analyzer");
    }

    [TestMethod]
    public void TestAReferenceWithNoOutputAssemblyIsNotCompiled() {
      CollectionAssert.DoesNotContain(CompiledProjects(), "NoAssembly");
    }
  }
}
