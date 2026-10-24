class T {
  /// <summary>Does a thing.</summary>
  /// @CSharpLua.Template = "foo({0})"
  public static extern void Foo(int x);

  /// @CSharpLua.Template = "bar({0})"
  /// <summary>Does a thing.</summary>
  public static extern void Bar(int x);

  /// <summary>Reads it.</summary>
  /// @CSharpLua.Get = "GET"
  public static int G { get { return 0; } }

  /// <summary>Puts it.</summary>
  /// <param name="x">The thing.</param>
  /// @CSharpLua.Template = "baz({0}, {1})"
  public static extern void Baz(int x, int y);
}

class U {
  static void Use() {
    T.Foo(1);
    T.Bar(2);
    var g = T.G;
    T.Baz(3, 4);
  }
}

class Errors {
  /// <summary>The locations of an error.</summary>
  /// @CSharpLua.Template = "TplWhere({0})"
  internal static string Where(System.Exception error) {
    return string.Empty;
  }

  /// @CSharpLua.Template = "TplPlain({0})"
  internal static string Plain(System.Exception error) {
    return string.Empty;
  }

  static string Run(System.Exception e) {
    return Where(e) + Plain(e);
  }
}
