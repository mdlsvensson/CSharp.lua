using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A switch is written as a repeat block, so the break a continue writes left only the switch, and the rest of the loop
  /// body ran. After a switch with a continue of its loop, the loop body is now left too.
  /// </summary>
  [TestClass]
  public class ContinueInSwitchTests {
    [TestMethod]
    public void TestContinueSkipsTheRestOfTheBody() {
      var output = GameScript.Run("ContinueInSwitch", "Program");
      Assert.AreEqual(
        "for 1\nfor 3\n"
        + "foreach 2\nforeach 3\n"
        + "while 1\nwhile 2\n"
        + "nested 2\n"
        + "finally 1\ntry 2\nfinally 2\n"
        + "inner 1\nouter 1\nouter 2\n",
        output);
    }
  }
}
