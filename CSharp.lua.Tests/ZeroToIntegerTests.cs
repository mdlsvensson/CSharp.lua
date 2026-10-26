using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// Warcraft III's Lua 5.3 gives math.ceil(0.0) and math.ceil(-0.0) as 1 (3.0.1, seen in the game), and CoreSystem truncated
  /// a float that was not above 0 with math.ceil, so (int)0f was 1 there. CoreSystem now takes its ceiling from math.floor.
  /// </summary>
  [TestClass]
  public class ZeroToIntegerTests {
    /// <summary>The game's math.ceil, as far as it was seen: wrong for a float zero only.</summary>
    private const string GameCeil = @"
local ceil = math.ceil
math.ceil = function (x)
  if x == 0 and math.type(x) == 'float' then return 1 end
  return ceil(x)
end";

    private const string Expected =
      "int 0f 0\nint -0f 0\nint 0.5f 0\nint -0.5f 0\nint 1f 1\nint -1.5f -1\nint 0.0 0\nlong 0.0 0\n"
      + "Floor 0\nRound 0\nRound -0.4f 0\nTruncate 0\n"
      + "Ceiling 0\nCeiling -0.0 0\nCeiling -0.5 0\nCeiling 0.5 1\nMathF.Ceiling 0\n"
      + "Round up 0\nRound to zero 0\nConvert 0\nDivide 0\n";

    [TestMethod]
    public void TestZeroBecomesIntegerZeroWithTheGamesCeil() {
      Assert.AreEqual(Expected, GameScript.RunAfter(GameCeil, "ZeroToInteger", "Program"));
    }

    [TestMethod]
    public void TestZeroBecomesIntegerZeroWithLuasCeil() {
      Assert.AreEqual(Expected, GameScript.Run("ZeroToInteger", "Program"));
    }
  }
}
