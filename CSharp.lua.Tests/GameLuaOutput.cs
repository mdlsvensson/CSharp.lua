using System.IO;

namespace CSharpLua.Tests {
  /// <summary>The Lua CSharp.lua writes for a sample under Input/GeneratedLua with the settings of a game's script (no debug library).</summary>
  internal static class GameLuaOutput {
    public static string Compile(string name) {
      var code = File.ReadAllText(Path.Combine(@"..\..\..\Input\GeneratedLua", name + ".cs"));
      return Compiler.CompileSingleCodeForGame(code);
    }
  }
}
