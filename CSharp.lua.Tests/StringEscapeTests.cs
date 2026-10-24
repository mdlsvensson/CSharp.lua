using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  [TestClass]
  public class StringEscapeTests {
    [TestMethod]
    public void TestEscapesLuaDoesNotKnowAreWrittenForLua() {
      var lua = LuaOutput.Compile("StringEscapes");
      StringAssert.Contains(lua, "return \"a\\027b\"");
      StringAssert.Contains(lua, "return \"a\\027\"");
      StringAssert.Contains(lua, "return \"a\\027\" .. n .. \"\\027\"");
    }

    [TestMethod]
    public void TestWideEscapesAreCharacters() {
      var lua = LuaOutput.Compile("StringEscapes");
      StringAssert.Contains(lua, "return \"a\uD83D\uDE00b\"");
      StringAssert.Contains(lua, "return \"a\u01bc\"");
      StringAssert.Contains(lua, "return \"aAg\"");
    }

    [TestMethod]
    public void TestNulBeforeADigitDoesNotMergeWithIt() {
      var lua = LuaOutput.Compile("StringEscapes");
      StringAssert.Contains(lua, "return \"a\\000b\"");
      StringAssert.Contains(lua, "return \"a\\0001\"");
    }

    [TestMethod]
    public void TestRawStringIsAString() {
      var lua = LuaOutput.Compile("StringEscapes");
      StringAssert.Contains(lua, "return [[raw \"text\"]]");
    }

    [TestMethod]
    public void TestInterpolatedTextIsEscaped() {
      var lua = LuaOutput.Compile("StringOdd");
      StringAssert.Contains(lua, "return \"a\\\\\" .. n .. \"\\\"q\\n\\t\" .. n");
    }

    [TestMethod]
    public void TestPercentInAFormattedStringIsDoubled() {
      var lua = LuaOutput.Compile("StringOdd");
      StringAssert.Contains(lua, "return ([[100%% %s \"q\" \\x]]):format(n)");
    }

    [TestMethod]
    public void TestVerbatimStringWithACarriageReturnIsQuoted() {
      // Lua reads every line end in a long string as \n, so \r\n has to be written out.
      var lua = Compiler.CompileSingleCode("class V { static string F() => @\"\r\nline\"; }");
      StringAssert.Contains(lua, "return \"\\r\\nline\"");
    }
  }
}
