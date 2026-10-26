using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// C# lets a block call a local function above its declaration, but the Lua defined the function where it was declared,
  /// so the call read a global of that name: nil, or a native of the same name. A local function used above its declaration
  /// is now defined before the first statement that uses it, with the locals it captures declared at the block's head.
  /// </summary>
  [TestClass]
  public class LocalFunctionBeforeDeclarationTests {
    // A global of the same name, as a native would be.
    private const string GlobalTwice = "function Twice(x) return -1 end";

    [TestMethod]
    public void TestALocalFunctionCalledAboveItsDeclarationRuns() {
      var output = GameScript.RunAfter(GlobalTwice, "LocalFunctionsBeforeDeclaration", "Program");
      Assert.AreEqual(
        "single 5\nglobal 8\nfact 120\nother 1\neven True True\nthrough 31\nabove 6\nabove 8\nbetween 1 7\n"
        + "lambda 16\nlambda hi\niterator 2\niterator 4\nloop 0:0\nloop 1:100\nblock -5\ngeneric 8 s\ninlined 8 10\nfirst 3\n",
        output);
    }

    [TestMethod]
    public void TestALocalFunctionDeclaredFirstIsWrittenAsBefore() {
      var lua = GameLuaOutput.Compile("LocalFunctionsBeforeDeclaration");
      Assert.IsTrue(lua.Contains("local function Three()"), lua);
    }

    [TestMethod]
    public void TestALocalDeclaredBetweenThatCannotMoveIsAnError() {
      var code = @"
class Program {
  static void Main() {
    System.Console.WriteLine(Show());
    int.TryParse(""5"", out var n);
    string Show() { n = 3; return ""x""; }
  }
}";
      var e = Assert.ThrowsException<CompilationErrorException>(() => Compiler.CompileSingleCodeForGame(code));
      StringAssert.Contains(e.Message, "the local function 'Show' is used above its declaration and uses 'n'");
    }
  }
}
