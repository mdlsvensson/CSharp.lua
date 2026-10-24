using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  [TestClass]
  public class TemplateAfterSummaryTests {
    [TestMethod]
    public void TestTemplateBelowASummaryIsUsed() {
      var lua = LuaOutput.Compile("TemplateAfterSummary");
      StringAssert.Contains(lua, "foo(1)");
      StringAssert.Contains(lua, "bar(2)");
      StringAssert.Contains(lua, "local g = GET");
      StringAssert.Contains(lua, "baz(3, 4)");
      StringAssert.Contains(lua, "return TplWhere(e) .. TplPlain(e)");
    }
  }
}
