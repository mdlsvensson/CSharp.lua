using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// The statements after a using declaration become a function given to System.using, whose caller passes on a return it
  /// flags. A return directly in that block was flagged, but one inside an if, a loop, a switch or a try was written as a
  /// plain return, so the method returned nil; and with two using declarations apart, the inner function's return was
  /// passed on unflagged. Every return inside such a function is now flagged, at any depth, except a lambda's or a local
  /// function's own.
  /// </summary>
  [TestClass]
  public class UsingDeclarationReturnTests {
    [TestMethod]
    public void TestANestedReturnAfterAUsingDeclarationReturnsItsValue() {
      var output = GameScript.Run("UsingDeclarationReturns", "Program", "D", "Quiet");
      Assert.AreEqual(
        "dispose if\ndispose if\n1 2\n"
        + "dispose loop\ndispose loop\n5 -1\n"
        + "dispose while\ndispose while\ndispose while\ndispose while\n30 30 20 0\n"
        + "dispose switch\ndispose switch\ndispose switch\none big other\n"
        + "early\ndispose void\nlate\ndispose void\n"
        + "1 2\n"
        + "between\ndispose second\ndispose first\nbetween\ndispose second\ndispose first\n1 2\n"
        + "dispose marked b\ndispose marked a\ndispose marked b\ndispose marked a\n1 2\n"
        + "dispose inner\ndispose outer\ndispose outer\n1 2\n"
        + "dispose before\n0 2\n"
        + "finally\ndispose try\nfinally\ndispose try\n1 2\n"
        + "dispose lambda\n10\n"
        + "dispose body 1\ndispose body 2\ndispose body 3\n3\n",
        output);
    }
  }
}
