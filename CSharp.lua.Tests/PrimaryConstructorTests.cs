using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A primary constructor's parameter read by a field or property initializer has its value there: the constructor keeps
  /// its parameters before the initializers run, as C# does, not after. A parameter kept by a class whose base class has a
  /// member or keeps a parameter of the same name is kept in a field of its own.
  /// </summary>
  [TestClass]
  public class PrimaryConstructorTests {
    [TestMethod]
    public void TestInitializersReadTheParameters() {
      var output = GameScript.Run("PrimaryConstructors", "Program", "Field", "Property", "Pair", "Base", "Derived", "Kept", "Early");
      Assert.AreEqual("5\n6\n7\nnn3\n14 7\n20 10\nx=8\n", output);
    }

    [TestMethod]
    public void TestSameNamedParametersOfABaseAreApart() {
      var output = GameScript.Run("PrimaryConstructorNames", "Program", "Base", "Derived", "Deeper", "WithField", "FieldNamed");
      Assert.AreEqual("q! q\nd?! d? d\n1 1\n", output);
    }

    [TestMethod]
    public void TestAParameterNoBaseHasKeepsItsName() {
      var lua = GameLuaOutput.Compile("PrimaryConstructorNames").Replace("\r\n", "\n");
      StringAssert.Contains(lua, "this.n = n\n");
      StringAssert.Contains(lua, "this.n_Derived = n\n");
    }

    [TestMethod]
    public void TestTheParameterIsKeptBeforeTheInitializer() {
      var lua = GameLuaOutput.Compile("PrimaryConstructors").Replace("\r\n", "\n");
      StringAssert.Contains(lua, "this.x = x\n      this.X = this.x\n");
    }
  }
}
