using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A foreach left early (a break, a return, a yield break) disposes its enumerator, as C# does, and an iterator's Dispose
  /// runs the finally blocks it is inside.
  /// </summary>
  [TestClass]
  public class IteratorDisposeTests {
    [TestMethod]
    public void TestFinallyRunsWhenTheLoopIsLeft() {
      var output = GameScript.Run("IteratorDispose", "Program");
      Assert.AreEqual(
        "1\nfinally break\n"
        + "finally return\n2\n"
        + "finally whole\n"
        + "2\nfinally where\n"
        + "10\n20\nfinally inner\nfinally outer\n"
        + "1\nfinally using\n"
        + "one\nafter switch 1\nafter switch 2\nfinally switch\n"
        + "2\n",
        output);
    }

    [TestMethod]
    public void TestALoopOverAListIsUnchanged() {
      var lua = GameLuaOutput.Compile("IteratorDispose").Replace("\r\n", "\n");
      StringAssert.Contains(lua, "for _, n in System.each(list) do\n        if n > 1 then\n          return n\n        end\n      end\n");
    }
  }
}
