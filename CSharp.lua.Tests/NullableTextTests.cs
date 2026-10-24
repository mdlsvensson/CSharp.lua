using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>A nullable value made text is written with System.toString, as a game's script has no debug library to give a number methods.</summary>
  [TestClass]
  public class NullableTextTests {
    [TestMethod]
    public void TestNullableJoinedToAString() {
      var lua = GameLuaOutput.Compile("NullableText");
      StringAssert.Contains(lua, "\"n=\" .. System.toString(n) .. \" f=\" .. System.toString(f) .. \" b=\" .. System.toString(b)");
      StringAssert.Contains(lua, "\" s=\" .. System.toString(s)");
      StringAssert.Contains(lua, "return System.toString(n) .. \"!\"");
    }

    [TestMethod]
    public void TestNullableReadThroughSafeNavigationJoinedToAString() {
      var lua = GameLuaOutput.Compile("NullableText");
      StringAssert.Contains(lua, "\"count \" .. System.toString(default) .. \" on \" .. System.toString(extern)");
    }

    [TestMethod]
    public void TestNullableAddedToAString() {
      var lua = GameLuaOutput.Compile("NullableText");
      StringAssert.Contains(lua, "text = System.toString(text) .. System.toString(n)");
      StringAssert.Contains(lua, "\"x\" .. System.toString((n and n + 1))");
    }

    [TestMethod]
    public void TestNullableInterpolated() {
      var lua = GameLuaOutput.Compile("NullableText");
      StringAssert.Contains(lua, "\"[\" .. System.toString(n) .. \"|\" .. System.toString(f) .. \"|\" .. System.toString(s) .. \"]\"");
      StringAssert.Contains(lua, "\"[\" .. System.toString(n, \"D3\") .. \"|\" .. System.toString(f, \"F1\") .. \"]\"");
    }

    [TestMethod]
    public void TestNullableCharIsACharacter() {
      var lua = GameLuaOutput.Compile("NullableText");
      StringAssert.Contains(lua, "(function(c) if c == nil then return \"\" end return string.char(c) end)(c)");
    }

    [TestMethod]
    public void TestFormatReachesTheFormatOverloadOfAStruct() {
      var lua = GameLuaOutput.Compile("NullableText");
      StringAssert.Contains(lua, "return s:ToString1(f, nil) end)(s, \"Q\")");
      StringAssert.Contains(lua, "t:ToString1(\"Q\", nil)");
    }

    [TestMethod]
    public void TestNoValueIsAskedForItsOwnToString() {
      var lua = GameLuaOutput.Compile("NullableText");
      Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch(lua, @"\b[nfbs]:ToString\(\)"), lua);
    }
  }
}
