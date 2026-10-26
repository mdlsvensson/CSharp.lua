using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// The negation of a negative constant was written with its minus next to the constant's: with const float F = -2f,
  /// -F was "--2.0 --[[Program.F]]", and Lua reads "--" as a comment, so the rest of the line was gone and the variable took
  /// the next line's value (or the script did not load). The same for -(int)E.Neg, an integer constant, and - -x. A minus
  /// before an operand that is written with a minus first now has a space after it.
  /// </summary>
  [TestClass]
  public class NegativeConstantNegationTests {
    [TestMethod]
    public void TestANegatedNegativeConstantKeepsItsLine() {
      var output = GameScript.Run("NegativeConstantNegation", "Program");
      Assert.AreEqual("2\n3.5\n1\n5\n5\n5\n4\n4\n14\n0.25\n1.5\n-2\n6\n-4\nafter\n", output);
    }

    [TestMethod]
    public void TestNoTwoMinusesAreWrittenTogether() {
      var lua = GameLuaOutput.Compile("NegativeConstantNegation");
      Assert.IsTrue(lua.Contains("local a = - -2.0"), lua);
      Assert.IsFalse(lua.Contains("= --"), lua);
    }
  }
}
