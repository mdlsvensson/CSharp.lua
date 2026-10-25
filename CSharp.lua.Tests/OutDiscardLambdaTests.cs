using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A lambda that returns nothing, with an expression body whose call keeps its result in a temporary (a call with an out
  /// argument, an object initializer), is written without the temporary's name as a statement of its own, which Lua refuses.
  /// </summary>
  [TestClass]
  public class OutDiscardLambdaTests {
    [TestMethod]
    public void TestTheScriptLoadsAndRuns() {
      var output = GameScript.Run("OutDiscardLambdas", "Program", "Terrain", "Point");
      Assert.AreEqual("stand 1\nstand 2\nstand 5\nTrue\nstand 6\nstand 7\n", output);
    }

    [TestMethod]
    public void TestNoTemporaryIsLeftAsAStatement() {
      var lua = GameLuaOutput.Compile("OutDiscardLambdas");
      foreach (var line in lua.Split('\n')) {
        var text = line.Trim();
        Assert.IsFalse(text is "default" or "extern" or "ref", "a bare temporary in\n" + lua);
      }
    }
  }
}
