using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  [TestClass]
  public class NullableGetValueOrDefaultTests {
    [TestMethod]
    public void TestGetValueOrDefaultNamesTheType() {
      var lua = LuaOutput.Compile("NullableGetValueOrDefault");
      Assert.IsFalse(lua.Contains("or:default()"), "the type of the default is missing");
      StringAssert.Contains(lua, "(n or System.Int32:default())");
      StringAssert.Contains(lua, "(b or System.Boolean:default())");
      StringAssert.Contains(lua, "(f or System.Single:default())");
      StringAssert.Contains(lua, "(p or Point:default())");
      StringAssert.Contains(lua, "(default.Count or System.Int32:default())");
    }

    [TestMethod]
    public void TestGetValueOrDefaultWithFallback() {
      var lua = LuaOutput.Compile("NullableGetValueOrDefault");
      StringAssert.Contains(lua, "(n or 7)");
    }
  }
}
