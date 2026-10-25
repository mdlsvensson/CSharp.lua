using System.Text.RegularExpressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// Each object initializer keeps its object in a temporary. Two in one statement got the same name when the first was
  /// read after the second was made (new P { X = 1 }.X + new P { X = 2 }.X, P c = new P { X = 3 }, d = new P { X = 4 }),
  /// so both read the second object.
  /// </summary>
  [TestClass]
  public class InitializerTempTests {
    [TestMethod]
    public void TestEachInitializerKeepsItsObject() {
      var output = GameScript.Run("InitializerTemps", "Program", "P");
      Assert.AreEqual("1 2\n12 34\n56\n15\n34\n1 2 3 4\n", output);
    }

    [TestMethod]
    public void TestAWholeValueStillGivesItsNameBack() {
      var lua = GameLuaOutput.Compile("InitializerTemps").Replace("\r\n", "\n");
      StringAssert.Matches(lua, new Regex(@"local (\w+) = P\(\)\n\s+\1\.X = 1\n\s+local a = \1\n\s+local \1 = P\(\)\n\s+\1\.X = 2\n\s+local b = \1\n"));
    }
  }
}
