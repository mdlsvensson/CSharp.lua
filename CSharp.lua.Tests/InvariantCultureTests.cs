using System;
using System.Globalization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// The compiler wrote a constant's number with the culture of the machine it ran on. A Swedish (or Norwegian, Finnish...)
  /// format writes a minus as U+2212, which Lua cannot read, so the script did not load. Numbers are now written the same on
  /// every machine.
  /// </summary>
  [TestClass]
  public class InvariantCultureTests {
    private static T InSwedish<T>(Func<T> action) {
      var culture = CultureInfo.CurrentCulture;
      var defaultCulture = CultureInfo.DefaultThreadCurrentCulture;
      var swedish = new CultureInfo("sv-SE");
      Assert.AreEqual("−", swedish.NumberFormat.NegativeSign);
      CultureInfo.CurrentCulture = swedish;
      CultureInfo.DefaultThreadCurrentCulture = swedish;
      try {
        return action();
      } finally {
        CultureInfo.CurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
      }
    }

    [TestMethod]
    public void TestNumbersAreWrittenInvariantUnderSwedish() {
      var lua = InSwedish(() => GameLuaOutput.Compile("InvariantCulture"));
      Assert.IsFalse(lua.Contains('−'), lua);
      Assert.IsTrue(lua.Contains("-300"), lua);
      Assert.IsTrue(lua.Contains("Below = -1"), lua);
    }

    [TestMethod]
    public void TestAScriptCompiledUnderSwedishRuns() {
      var output = InSwedish(() => GameScript.Run("InvariantCulture", "Program", "Kind"));
      Assert.AreEqual(
        "-300 -70000 -1 -1\n"
        + "-2147483648 -600 -9 -3\n"
        + "True\n"
        + "-0.5 -1.25 97\n"
        + "Above Below\n",
        output);
    }
  }
}
