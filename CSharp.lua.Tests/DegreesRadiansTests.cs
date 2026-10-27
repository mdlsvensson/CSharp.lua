using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// CoreSystem's Natives.lua puts Lua's math functions in place of some of the game's natives, and it had Deg2Rad as
  /// math.deg and Rad2Deg as math.rad, the wrong way round, so Natives.Deg2Rad(180f) gave 10313.24 in a map.
  /// </summary>
  [TestClass]
  public class DegreesRadiansTests {
    [TestMethod]
    public void TestDeg2RadAndRad2DegConvertTheRightWay() {
      var printed = CoreSystemLua.Run(@"
print(string.format('%.4f', Deg2Rad(180)))
print(string.format('%.4f', Rad2Deg(math.pi)))
print(string.format('%.4f', Deg2Rad(90)))");
      Assert.AreEqual("3.1416\n180.0000\n1.5708\n", printed);
    }
  }
}
