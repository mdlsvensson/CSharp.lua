using System.IO;
using System.Linq;

namespace CSharpLua.Tests {
  /// <summary>Runs a sample under Input/GeneratedLua as a game's script: compiled without the debug library, loaded on CoreSystem, its Program.Main called.</summary>
  internal static class GameScript {
    /// <summary>Returns what Program.Main of the sample printed, a line each; <paramref name="types"/> are the sample's classes, as System.init takes them.</summary>
    public static string Run(string name, params string[] types) {
      var lua = GameLuaOutput.Compile(name);
      var init = "System.init({ types = { " + string.Join(", ", types.Select(type => "\"" + type + "\"")) + " } })";
      return CoreSystemLua.Run(lua + "\n" + init + "\nProgram.Main()\n");
    }
  }
}
