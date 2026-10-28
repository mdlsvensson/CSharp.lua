using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A conditional added to an element or a property lost its parentheses: numbers[0] += yes ? 1 : 0 was written
  /// numbers:set(0, numbers:get(0) + yes and 1 or 0), which Lua reads as (numbers:get(0) + yes) and 1 or 0, so it stopped
  /// on arithmetic on a boolean. The same for a list, a dictionary, a two-dimensional array and a property with a getter
  /// and setter; a local or a field kept them. The right side of a compound assignment is now put in parentheses the same
  /// way on every path.
  /// </summary>
  [TestClass]
  public class CompoundConditionalTests {
    [TestMethod]
    public void TestAConditionalAddedToAnElementOrPropertyIsAddedWhole() {
      var output = GameScript.Run("CompoundConditional", "Program");
      Assert.AreEqual("6\n3\n10\n6\n4\n6\n8\n6\nax\n6\n9\n", output);
    }

    [TestMethod]
    public void TestTheConditionalIsInParentheses() {
      var lua = GameLuaOutput.Compile("CompoundConditional");
      Assert.IsTrue(lua.Contains("numbers:get(0) + (yes and 1 or 0)"), lua);
      Assert.IsFalse(lua.Contains("+ yes and"), lua);
      Assert.IsFalse(lua.Contains("(("), lua);
    }
  }
}
