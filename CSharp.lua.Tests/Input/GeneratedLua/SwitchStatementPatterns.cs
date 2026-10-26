using System;

class Box {
  public Box Inner;
  public int X;
  public int Y { get; set; }
  public int? Maybe;
  public Box Next { get; set; }
}

record Pair(int A, int B);

struct Spot {
  public int X;
}

class Program {
  static string Relational(int n) {
    switch (n) {
      case < 0: return "neg";
      case >= 1 and <= 5: return "small";
      case > 5: return "big";
      default: return "zero";
    }
  }

  static string Parenthesized(int n) {
    switch (n) {
      case (1 or 2): return "low";
      case not (> 2 and < 9): return "out";
      default: return "mid";
    }
  }

  static string Not(Box b) {
    switch (b) {
      case not null: return "box";
      default: return "null";
    }
  }

  static string NullableRelational(int? n) {
    switch (n) {
      case > 5: return "big";
      case null: return "none";
      default: return "small";
    }
  }

  static string ObjectRelational(object o) {
    switch (o) {
      case > 5: return "big int";
      case string s: return "string " + s;
      case int: return "int";
      default: return "other";
    }
  }

  static string Properties(Box b) {
    switch (b) {
      case { Inner.X: 9 }: return "inner nine";
      case { X: not 3, Y: > 10 }: return "x not 3, y big";
      case { X: 1 or 2 }: return "x low";
      case { Inner: { Y: 4 } }: return "inner y four";
      case { Maybe: > 0 }: return "maybe positive";
      case { Next.Y: 5 }: return "next y five";
      case { X: 3 } box when box.Y == 7: return "three seven";
      case { }: return "other box";
      default: return "null";
    }
  }

  static string Positional(Pair p) {
    switch (p) {
      case (1, var b) when b > 5: return "one big " + b;
      case (var a, > 3): return "b big a " + a;
      case (_, 0): return "b zero";
      default: return "pair";
    }
  }

  static string Tuple(int x, int y) {
    switch ((x, y)) {
      case (1, _): return "x one";
      case (_, > 4): return "y big";
      default: return "tuple";
    }
  }

  static string Var(int n) {
    switch (n) {
      case var v when v % 2 == 0: return "even " + v;
      case int odd and > 10: return "big odd " + odd;
      default: return "odd";
    }
  }

  static string Struct(Spot s) {
    switch (s) {
      case { X: > 2 }: return "far";
      default: return "near";
    }
  }

  // Sections fall through to the statements after the switch.
  static string Statements(int n) {
    string text = "";
    switch (n) {
      case < 3:
        text = "low";
        break;
      case > 7:
        text = "high";
        break;
    }
    return text + ".";
  }

  public static void Main() {
    Console.WriteLine(Relational(-3) + " " + Relational(3) + " " + Relational(9) + " " + Relational(0));
    Console.WriteLine(Parenthesized(2) + " " + Parenthesized(12) + " " + Parenthesized(5));
    Console.WriteLine(Not(new Box()) + " " + Not(null));
    Console.WriteLine(NullableRelational(7) + " " + NullableRelational(null) + " " + NullableRelational(2));
    Console.WriteLine(ObjectRelational(7) + " " + ObjectRelational("s") + " " + ObjectRelational(2) + " " + ObjectRelational(2.5f));
    Console.WriteLine(Properties(new Box { Inner = new Box { X = 9 } }));
    Console.WriteLine(Properties(new Box { X = 4, Y = 11 }));
    Console.WriteLine(Properties(new Box { X = 2 }));
    Console.WriteLine(Properties(new Box { X = 3, Inner = new Box { Y = 4 } }));
    Console.WriteLine(Properties(new Box { X = 3, Maybe = 2 }));
    Console.WriteLine(Properties(new Box { X = 3, Next = new Box { Y = 5 } }));
    Console.WriteLine(Properties(new Box { X = 3, Y = 7 }));
    Console.WriteLine(Properties(new Box { X = 3 }));
    Console.WriteLine(Properties(null));
    Console.WriteLine(Positional(new Pair(1, 6)) + " | " + Positional(new Pair(2, 4)) + " | " + Positional(new Pair(1, 0)) + " | " + Positional(new Pair(1, 2)));
    Console.WriteLine(Tuple(1, 9) + " " + Tuple(2, 9) + " " + Tuple(2, 2));
    Console.WriteLine(Var(4) + " " + Var(11) + " " + Var(3));
    Console.WriteLine(Struct(new Spot { X = 5 }) + " " + Struct(new Spot()));
    Console.WriteLine(Statements(1) + " " + Statements(9) + " " + Statements(5));
  }
}
