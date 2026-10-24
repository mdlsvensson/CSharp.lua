using System.IO;

namespace CSharpLua.Tests {
  /// <summary>The Lua CSharp.lua writes for a sample under Input/GeneratedLua, for tests of cases where the compiler does not fail but writes wrong Lua.</summary>
  internal static class LuaOutput {
    public static string Compile(string name) {
      var code = File.ReadAllText(Path.Combine(@"..\..\..\Input\GeneratedLua", name + ".cs"));
      return Compiler.CompileSingleCode(code);
    }
  }
}
