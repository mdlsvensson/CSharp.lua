using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// Lua does integer arithmetic on a value it holds as an integer (and wraps it past 2^31), so a float or double has to be a Lua
  /// float, with a point, wherever it is written or made.
  /// </summary>
  [TestClass]
  public class WholeFloatTests {
    private static string Lua() => GameLuaOutput.Compile("WholeFloats");

    [TestMethod]
    public void TestWholeFloatLiteralsHaveAPoint() {
      var lua = Lua();
      StringAssert.Contains(lua, "return 1.0\n".Replace("\n", System.Environment.NewLine));
      StringAssert.Contains(lua, "System.Double.GetHashCode((100.0)) + 3.0");
      StringAssert.Contains(lua, "return -1.0");
      StringAssert.Contains(lua, "return 0.0");
      StringAssert.Contains(lua, "ArraySingle { 1.0, 2.0 }");
    }

    [TestMethod]
    public void TestFoldedConstantsHaveAPoint() {
      var lua = Lua();
      StringAssert.Contains(lua, "Big = 16777216.0 --[[16777216f + 1f]]");
      StringAssert.Contains(lua, "return 10.0 --[[K * 2]]");
    }

    [TestMethod]
    public void TestDefaultValuesHaveAPoint() {
      var lua = Lua();
      StringAssert.Contains(lua, "field = 0.0,");
      StringAssert.Contains(lua, "Default(2.0) + Default(3.0)");
    }

    [TestMethod]
    public void TestAnIntegerMadeAFloatIsAdded0() {
      var lua = Lua();
      StringAssert.Contains(lua, "return (count + 0.0)");
      StringAssert.Contains(lua, "local f = (count + 0.0)");
      StringAssert.Contains(lua, "f = (count + 0.0)");
      StringAssert.Contains(lua, "return (n + 0.0)");
      StringAssert.Contains(lua, "(a + 0.0) * b + 0.5");
      StringAssert.Contains(lua, "(a + 0.0) / b");
    }

    [TestMethod]
    public void TestAnIntegerBesideAFloatIsLeftAlone() {
      var lua = Lua();
      StringAssert.Contains(lua, "f = f + count");
      StringAssert.Contains(lua, "return f * n");
    }

    [TestMethod]
    public void TestANullableIntegerMadeAFloatKeepsNil() {
      var lua = Lua();
      StringAssert.Contains(lua, "return n and (n + 0.0)");
    }

    [TestMethod]
    public void TestAFloatInAStringIsMadeTextByCoreSystem() {
      var lua = Lua();
      StringAssert.Contains(lua, "\"x\" .. System.toString(4.0) .. System.toString(1.5)");
    }

    [TestMethod]
    public void TestSingleAndDoubleStartAtFloatZero() {
      var output = CoreSystemLua.Run(@"
        print(math.type(System.Single:default()), math.type(System.Double:default()))
        print(math.type(System.Array(System.Single)(2):get(0)))");
      Assert.AreEqual("float\tfloat\nfloat\n", output);
    }

    [TestMethod]
    public void TestFloorAndCeilingGiveFloats() {
      var output = CoreSystemLua.Run(@"
        print(math.type(math.Floor(2.5)), math.Floor(2.5), math.type(math.Ceiling(2.5)), math.Ceiling(2.5))
        print(math.Floor(-0.5), math.Ceiling(-1.5))");
      Assert.AreEqual("float\t2.0\tfloat\t3.0\n-1.0\t-1.0\n", output);
    }

    [TestMethod]
    public void TestAWholeFloatIsTextWithoutAPoint() {
      var output = CoreSystemLua.Run(@"
        print(System.toString(4.0), System.toString(-3.0), System.toString(2.5), System.toString(7))
        print(System.Number.ToString(5.0))");
      Assert.AreEqual("4\t-3\t2.5\t7\n5\n", output);
    }

    [TestMethod]
    public void TestFloatArithmeticDoesNotWrapPast2To31() {
      var output = CoreSystemLua.Run(@"
        local big = 2147483647.0
        print(big + 1.0, math.type(big + 1.0))");
      Assert.AreEqual("2147483648.0\tfloat\n", output);
    }
  }
}
