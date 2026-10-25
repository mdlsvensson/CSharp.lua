using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// Lua takes a return only as the last statement of a block. A return followed by a comment in a loop with a continue, by
  /// a for loop's step, or by statements that never run, is written so the script still loads.
  /// </summary>
  [TestClass]
  public class ReturnLastInBlockTests {
    [TestMethod]
    public void TestTheScriptLoadsAndRuns() {
      var output = GameScript.Run("ReturnLastInBlock", "Program");
      Assert.AreEqual("measure\npause\nnamed\n1\n2\n1\n3\n1\n10\n7\n1\nlambda\n", output);
    }

    [TestMethod]
    public void TestNoContinueIsSetAfterTheLastReturn() {
      var lua = GameLuaOutput.Compile("ReturnLastInBlock").Replace("\r\n", "\n");
      StringAssert.Contains(lua, "return\n          --@mw 1:24\n        until 1");
    }

    [TestMethod]
    public void TestAReturnBeforeOtherStatementsIsInADoBlock() {
      var lua = GameLuaOutput.Compile("ReturnLastInBlock").Replace("\r\n", "\n");
      StringAssert.Contains(lua, "do\n            return i\n          end\n          i = i + 2");
      StringAssert.Contains(lua, "do\n          return 1\n        end\n        System.Console.WriteLine(\"never\")");
    }
  }
}
