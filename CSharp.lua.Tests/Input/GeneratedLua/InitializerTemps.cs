using System;

class P {
  public int X;

  public int With(P other) => X * 10 + other.X;

  public static P operator +(P a, P b) => new P { X = a.X + b.X };
}

class Program {
  static int Pair(int a, int b) => a * 10 + b;

  static int Returned() {
    return new P { X = 3 }.X * 10 + new P { X = 4 }.X;
  }

  public static void Main() {
    // Several initializers in one statement, each read after it is made.
    Console.WriteLine(new P { X = 1 }.X + " " + new P { X = 2 }.X);
    Console.WriteLine(new P { X = 1 }.With(new P { X = 2 }) + " " + new P { X = 3 }.With(new P { X = 4 }));
    Console.WriteLine(Pair(new P { X = 5 }.X, new P { X = 6 }.X));
    Console.WriteLine((new P { X = 7 } + new P { X = 8 }).X);
    Console.WriteLine(Returned());

    // Initializers that are a statement's whole value, which were right.
    var a = new P { X = 1 };
    var b = new P { X = 2 };
    P c = new P { X = 3 }, d = new P { X = 4 };
    Console.WriteLine(a.X + " " + b.X + " " + c.X + " " + d.X);
  }
}
