using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  [TestClass]
  public class NullableEnumToStringTests {
    [TestMethod]
    public void TestSafeNavigationToStringOnNullableEnum() {
      var lua = LuaOutput.Compile("NullableEnumToString");
      StringAssert.Contains(lua, "default = System.EnumToString(default, Color)");
      Assert.IsFalse(lua.Contains(":EnumToString("), "a number has no methods in the game's Lua");
    }

    [TestMethod]
    public void TestToStringOnNullableEnumStillWorks() {
      var lua = LuaOutput.Compile("NullableEnumToString");
      StringAssert.Contains(lua, "return System.EnumToString(c, Color)");
    }
  }
}
