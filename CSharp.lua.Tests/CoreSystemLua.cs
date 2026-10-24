using System;
using System.Diagnostics;
using System.IO;
using System.Text;

using CSharpLua.CoreSystem;

namespace CSharpLua.Tests {
  /// <summary>Runs Lua on CoreSystem, the Lua runtime of compiled code, with the Lua 5.3 of test/__bin and no debug library, as in a Warcraft III map.</summary>
  internal static class CoreSystemLua {
    private static readonly string Root = Path.GetFullPath(@"..\..\..\..");

    /// <summary>Loads CoreSystem, runs <paramref name="code"/> and returns what it printed, a line each.</summary>
    public static string Run(string code) {
      var coreSystem = Path.Combine(Root, "CSharp.lua", "CoreSystem.Lua");
      var script = new StringBuilder();
      script.AppendLine("debug = nil");
      foreach (var file in CoreSystemProvider.GetCoreSystemFiles()) {
        script.AppendLine("dofile([[" + file + "]])");
      }
      script.AppendLine(code);

      var path = Path.Combine(Path.GetTempPath(), "coresystem-" + Guid.NewGuid().ToString("N") + ".lua");
      File.WriteAllText(path, script.ToString());
      try {
        var start = new ProcessStartInfo(Path.Combine(Root, "test", "__bin", "lua5.3", "lua.exe"), "\"" + path + "\"") {
          WorkingDirectory = coreSystem,
          RedirectStandardOutput = true,
          RedirectStandardError = true,
          UseShellExecute = false,
        };
        using var process = Process.Start(start);
        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();
        process.WaitForExit();
        if (process.ExitCode != 0) {
          throw new InvalidOperationException(error);
        }
        return output.Replace("\r\n", "\n");
      } finally {
        File.Delete(path);
      }
    }
  }
}
