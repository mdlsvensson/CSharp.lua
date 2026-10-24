using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>c ? a : b is written c and a or b only when a can be neither nil nor false: otherwise Lua gives b where C# gives a.</summary>
  [TestClass]
  public class NullableConditionalTests {
    // The text of a method of the sample as the Lua writes it: from its name to the next method, or to the class's table.
    private static string Body(string lua, string method) {
      var start = lua.IndexOf(" " + method + " = function (");
      Assert.IsTrue(start >= 0, method + " is not in\n" + lua);
      var ends = new[] { lua.IndexOf(" = function (", start + 20), lua.IndexOf("return {", start) }.Where(i => i > 0).ToList();
      return lua.Substring(start, (ends.Count == 0 ? lua.Length : ends.Min()) - start);
    }

    [TestMethod]
    public void TestNullableFirstBranchIsAnIf() {
      var lua = LuaOutput.Compile("NullableConditional");
      foreach (var method in new[] { "Pick", "PickBool", "NullElse", "Checked", "FromPlain", "Struct", "Property" }) {
        var body = Body(lua, method);
        Assert.IsTrue(body.Contains("if "), method + ":" + body);
        Assert.IsFalse(body.Contains(" and "), method + ":" + body);
      }
    }

    [TestMethod]
    public void TestStructTypeParameterFirstBranchIsAnIf() {
      var lua = LuaOutput.Compile("NullableConditional");
      var constrained = lua.Substring(lua.IndexOf("namespace.class(\"Constrained\""));
      Assert.IsTrue(constrained.Contains("if c then"), constrained);
      Assert.IsFalse(constrained.Contains(" and "), constrained);
    }

    [TestMethod]
    public void TestPlainValueFirstBranchStaysAndOr() {
      var lua = LuaOutput.Compile("NullableConditional");
      StringAssert.Contains(Body(lua, "Plain"), "return c and a or b");
    }

    [TestMethod]
    public void TestReferenceAndBoolFirstBranchIsAnIf() {
      var lua = LuaOutput.Compile("NullableConditional");
      Assert.IsTrue(Body(lua, "Reference").Contains("if c then"));
      Assert.IsTrue(Body(lua, "PlainBool").Contains("if c then"));
    }
  }
}
