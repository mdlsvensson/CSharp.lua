using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A switch statement's case label wrote a pattern that was not a constant, a type, a declaration, var, a property or a
  /// positional pattern as an equality with the pattern's value: case &lt; 0: was == 0 and case &gt; 5: was == 5. An extended
  /// property pattern (case { Inner.X: 9 }:) was always false, and case not null:, case (1 or 2): and case { X: not 3 }:
  /// stopped the compiler. A case label's pattern is now written as an is expression's is, a pattern inside a property or
  /// positional pattern is checked as one, a property pattern does not match null, and a relational pattern checks a value
  /// that can be nil or of another type first. An is expression's positional pattern declares the locals it reads (on a
  /// value type). A variable declared for a member stops the compiler with an error.
  /// </summary>
  [TestClass]
  public class SwitchStatementPatternTests {
    [TestMethod]
    public void TestPatternsInCaseLabels() {
      var output = GameScript.Run("SwitchStatementPatterns", "Program", "Box", "Pair", "Spot");
      Assert.AreEqual(
        "neg small big zero\n"
        + "low out mid\n"
        + "box null\n"
        + "big none small\n"
        + "big int string s int other\n"
        + "inner nine\nx not 3, y big\nx low\ninner y four\nmaybe positive\nnext y five\nthree seven\nother box\nnull\n"
        + "one big 6 | b big a 2 | b zero | pair\n"
        + "x one y big tuple\n"
        + "even 4 big odd 11 odd\n"
        + "far near\n"
        + "low. high. .\n"
        + "True True True True | False False False False\n",
        output);
    }

    [TestMethod]
    public void TestAVariableForAMemberIsAnError() {
      var code = @"
class Box { public int X; }
class Program {
  static void Main() {
    switch (new Box()) {
      case { X: var x }: System.Console.WriteLine(x); break;
    }
  }
}";
      var e = Assert.ThrowsException<CompilationErrorException>(() => Compiler.CompileSingleCodeForGame(code));
      Assert.IsTrue(e.Message.Contains("the variable 'x' declared for a member in a property pattern is not supported"), e.Message);
    }
  }
}
