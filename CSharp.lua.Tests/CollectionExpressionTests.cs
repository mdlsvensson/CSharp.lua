using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  [TestClass]
  public class CollectionExpressionTests {
    [TestMethod]
    public void TestCollectionExpressionsAreWritten() {
      var lua = LuaOutput.Compile("CollectionExpressions");
      StringAssert.Contains(lua, "return ArrayInt32 { 1, 2, 3 }");
      StringAssert.Contains(lua, "field = ListInt32(ArrayInt32 { 1, 2 })");
      StringAssert.Contains(lua, "default:AddRange(a)");
      StringAssert.Contains(lua, "local a = ListInt32(ArrayInt32 { 1 })");
      StringAssert.Contains(lua, "local b = ArrayInt32 {}");
      StringAssert.Contains(lua, "a = ListInt32(ArrayInt32 { 4, 5 })");
      StringAssert.Contains(lua, "Take(ArrayInt32 { 7, 8 })");
    }
  }
}
