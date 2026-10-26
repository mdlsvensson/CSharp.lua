using System.Text.RegularExpressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A struct with its own == compared with null is C#'s lifted operator, which never calls the struct's operator: the
  /// answer is whether the value is null. The Lua called the operator with nil, so n == null was false for a null n and
  /// stopped the script for a value ("attempt to index a nil value"). It is now n == nil (n ~= nil for !=), on either side.
  /// </summary>
  [TestClass]
  public class NullableEqualsNullTests {
    [TestMethod]
    public void TestANullableStructComparedWithNull() {
      var output = GameScript.Run("NullableEqualsNull", "Program", "P");
      Assert.AreEqual(
        "True False True False True\n"
        + "False True False True False\n"
        + "True False False True False True\n"
        + "True False False True\n"
        + "False True True False\n"
        + "False True\n"
        + "found 7\n",
        output);
    }

    [TestMethod]
    public void TestTheOperatorIsNotCalledWithNil() {
      var lua = GameLuaOutput.Compile("NullableEqualsNull");
      Assert.IsFalse(Regex.IsMatch(lua, @"op_(In)?[Ee]quality\((nil, |[^)]*, nil\))"), lua);
    }
  }
}
