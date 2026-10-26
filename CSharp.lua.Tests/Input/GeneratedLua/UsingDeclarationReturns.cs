using System;
using System.Collections.Generic;

class D : IDisposable {
  private readonly string name;
  public D(string name) { this.name = name; }
  public void Dispose() { Console.WriteLine("dispose " + name); }
}

// Two using declarations in a row are disposed in the order they were made, not C#'s; this one says nothing.
class Quiet : IDisposable {
  public void Dispose() { }
}

class Program {
  static int InIf(bool early) {
    using var d = new D("if");
    if (early) {
      return 1;
    }
    return 2;
  }

  static int InLoop(int[] xs) {
    using var d = new D("loop");
    foreach (var x in xs) {
      if (x > 1) {
        return x;
      }
    }
    return -1;
  }

  static int InWhileAndElse(int n) {
    using var d = new D("while");
    while (n > 0) {
      if (n == 3) {
        return 30;
      } else if (n == 2) {
        return 20;
      }
      n--;
    }
    return 0;
  }

  static string InSwitch(int n) {
    using var d = new D("switch");
    switch (n) {
      case 1:
        return "one";
      default:
        if (n > 5) {
          return "big";
        }
        break;
    }
    return "other";
  }

  static void Void(bool early) {
    using var d = new D("void");
    if (early) {
      Console.WriteLine("early");
      return;
    }
    Console.WriteLine("late");
  }

  static int TwoTogether(bool early) {
    using var a = new Quiet();
    using var b = new Quiet();
    if (early) {
      return 1;
    }
    return 2;
  }

  static int TwoApart(bool early) {
    using var a = new D("first");
    Console.WriteLine("between");
    using var b = new D("second");
    if (early) {
      return 1;
    }
    return 2;
  }

  // Line marks (as Moonwell puts them) between two using declarations keep them apart.
  static int TwoMarked(bool early) {
    /*[[--@mw 1:1]]*/ using var a = new D("marked a");
    /*[[--@mw 1:2]]*/ using var b = new D("marked b");
    /*[[--@mw 1:3]]*/ if (early) {
      /*[[--@mw 1:4]]*/ return 1;
    }
    /*[[--@mw 1:5]]*/ return 2;
  }

  static int Inner(bool early) {
    using var a = new D("outer");
    if (early) {
      using var b = new D("inner");
      if (early) {
        return 1;
      }
    }
    return 2;
  }

  // A return before the using declaration is the method's own.
  static int Before(bool early) {
    if (early) {
      return 0;
    }
    using var d = new D("before");
    return 2;
  }

  static int InTry(bool early) {
    using var d = new D("try");
    try {
      if (early) {
        return 1;
      }
    } finally {
      Console.WriteLine("finally");
    }
    return 2;
  }

  // A lambda's and a local function's returns are their own.
  static int Lambdas() {
    using var d = new D("lambda");
    Func<int, int> f = x => {
      if (x > 0) {
        return x * 2;
      }
      return 0;
    };
    int Local(int x) {
      if (x > 0) {
        return x + 1;
      }
      return 0;
    }
    if (f(3) == 6) {
      return f(4) + Local(1);
    }
    return 0;
  }

  static int InLoopBody(List<int> xs) {
    int sum = 0;
    foreach (var x in xs) {
      using var d = new D("body " + x);
      if (x == 3) {
        return sum;
      }
      sum += x;
    }
    return -1;
  }

  public static void Main() {
    Console.WriteLine(InIf(true) + " " + InIf(false));
    Console.WriteLine(InLoop(new[] { 1, 5, 7 }) + " " + InLoop(new[] { 0 }));
    Console.WriteLine(InWhileAndElse(3) + " " + InWhileAndElse(4) + " " + InWhileAndElse(2) + " " + InWhileAndElse(1));
    Console.WriteLine(InSwitch(1) + " " + InSwitch(9) + " " + InSwitch(3));
    Void(true);
    Void(false);
    Console.WriteLine(TwoTogether(true) + " " + TwoTogether(false));
    Console.WriteLine(TwoApart(true) + " " + TwoApart(false));
    Console.WriteLine(TwoMarked(true) + " " + TwoMarked(false));
    Console.WriteLine(Inner(true) + " " + Inner(false));
    Console.WriteLine(Before(true) + " " + Before(false));
    Console.WriteLine(InTry(true) + " " + InTry(false));
    Console.WriteLine(Lambdas());
    Console.WriteLine(InLoopBody(new List<int> { 1, 2, 3, 4 }));
  }
}
