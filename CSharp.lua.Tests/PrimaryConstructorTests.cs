using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A primary constructor's parameter read by a field or property initializer has its value there: the constructor keeps
  /// its parameters before the initializers run, as C# does, not after.
  /// </summary>
  [TestClass]
  public class PrimaryConstructorTests {
    [TestMethod]
    public void TestInitializersReadTheParameters() {
      var output = GameScript.Run("PrimaryConstructors", "Program", "Field", "Property", "Pair", "Base", "Derived", "Kept", "Early");
      Assert.AreEqual("5\n6\n7\nnn3\n14 7\n20 10\nx=8\n", output);
    }

    [TestMethod]
    public void TestTheParameterIsKeptBeforeTheInitializer() {
      var lua = GameLuaOutput.Compile("PrimaryConstructors").Replace("\r\n", "\n");
      StringAssert.Contains(lua, "this.x = x\n      this.X = this.x\n");
    }
  }
}
