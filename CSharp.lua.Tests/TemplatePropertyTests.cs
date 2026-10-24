using System.Text.RegularExpressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  [TestClass]
  public class TemplatePropertyTests {
    [TestMethod]
    public void TestStaticTemplatePropertyThroughUsingStatic() {
      var lua = LuaOutput.Compile("TemplateProperties");
      Assert.IsTrue(lua.Contains("return ATTACK_TYPE_MAGIC"), lua);
    }

    [TestMethod]
    public void TestInstanceTemplatePropertyRead() {
      // Bare, after this., after base., and bare in a derived class.
      var lua = LuaOutput.Compile("TemplateReceivers");
      Assert.AreEqual(4, Regex.Matches(lua, @"return Widget\.Default\(getmetatable\(this\)\)").Count, lua);
    }

    [TestMethod]
    public void TestInstanceTemplatePropertyPut() {
      var lua = LuaOutput.Compile("TemplateReceivers");
      Assert.AreEqual(4, Regex.Matches(lua, @"this\.depth = [3456]").Count, lua);
    }

    [TestMethod]
    public void TestStaticTemplatePropertyInItsOwnClass() {
      var lua = LuaOutput.Compile("TemplateReceivers");
      Assert.IsTrue(lua.Contains("return MAGIC"), lua);
    }

    [TestMethod]
    public void TestInstanceTemplatePropertyInAnObjectInitializer() {
      var lua = LuaOutput.Compile("TemplateReceivers");
      Assert.IsTrue(Regex.IsMatch(lua, @"\.depth = 7"), lua);
    }
  }
}
