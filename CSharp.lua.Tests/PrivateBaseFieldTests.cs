using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A class's member named as a private field of a base class (an instance field, an auto-property kept as a field or a
  /// field-like event) was given the same Lua name, and an object keeps both in its one table, so the two were one value.
  /// Such a member now gets a name of its own, as one named as a protected field did. Private methods are not changed:
  /// they are called as locals, so a derived class's method of the same name keeps its name.
  /// </summary>
  [TestClass]
  public class PrivateBaseFieldTests {
    [TestMethod]
    public void TestAPrivateBaseFieldIsApart() {
      var output = GameScript.Run("PrivateBaseFields", "Program", "Base", "Derived", "Public", "Middle", "Deeper");
      Assert.AreEqual(
        "1 100\n10 7\nbase changed\nderived changed\n2 3\n1 200\n1 300\n",
        output);
    }

    [TestMethod]
    public void TestAPrivateMethodKeepsItsName() {
      var lua = GameLuaOutput.Compile("PrivateBaseFields");
      Assert.IsFalse(lua.Contains("Twice1"));
      Assert.IsTrue(lua.Contains("this.count1"));
    }
  }
}
