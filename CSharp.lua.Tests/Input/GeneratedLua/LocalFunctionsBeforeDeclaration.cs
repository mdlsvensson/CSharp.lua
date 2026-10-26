using System;
using System.Collections.Generic;

class Program {
  // The only local function of the method, called above its declaration.
  static void Single() {
    int total = Add(2, 3);
    Console.WriteLine("single " + total);

    int Add(int a, int b) => a + b;
  }

  // Named as a global of the script (the test gives the script a Twice that answers -1).
  static void NamedAsGlobal() {
    Console.WriteLine("global " + Twice(4));

    int Twice(int x) => x * 2;
  }

  static void Recursive() {
    Console.WriteLine("fact " + Fact(5));
    Console.WriteLine("other " + Other());

    int Fact(int n) => n <= 1 ? 1 : n * Fact(n - 1);
    int Other() => 1;
  }

  static void Mutual() {
    Console.WriteLine("even " + IsEven(10) + " " + IsOdd(7));

    bool IsEven(int n) => n == 0 || IsOdd(n - 1);
    bool IsOdd(int n) => n != 0 && IsEven(n - 1);
  }

  // A local function called only through another one that is called early.
  static void Through() {
    Console.WriteLine("through " + Outer(3));

    int Outer(int n) => Inner(n) + 1;
    int Inner(int n) => n * 10;
  }

  // Captures a local declared above the call.
  static void CapturedAbove() {
    int scale = 3;
    Console.WriteLine("above " + Scale(2));
    scale = 4;
    Console.WriteLine("above " + Scale(2));

    int Scale(int v) => v * scale;
  }

  // Captures locals declared between the call and the declaration: the calls above only write them.
  static void CapturedBetween() {
    Reset();
    int level = 5;
    Reset();
    int count;
    Mark();
    Console.WriteLine("between " + level + " " + count);

    void Reset() { level = 1; }
    void Mark() { count = level + 6; }
  }

  // Inside a lambda, and used through a lambda made above the declaration.
  static void InLambda() {
    Func<int> square = () => {
      int v = Sq(4);
      return v;

      int Sq(int x) => x * x;
    };
    Console.WriteLine("lambda " + square());

    Func<string> hello = () => Hello();
    Console.WriteLine("lambda " + hello());

    string Hello() => "hi";
  }

  static IEnumerable<int> Numbers() {
    yield return Inc(1);
    yield return Inc(Inc(2));

    int Inc(int x) => x + 1;
  }

  static void InIterator() {
    foreach (var n in Numbers()) {
      Console.WriteLine("iterator " + n);
    }
  }

  // In a nested block and a loop body.
  static void Nested() {
    for (int i = 0; i < 2; i++) {
      int seen = i * 100;
      Console.WriteLine("loop " + Show(i));

      string Show(int n) => n + ":" + seen;
    }
    if (true) {
      Console.WriteLine("block " + Neg(5));

      int Neg(int n) => -n;
    }
  }

  static void Generic() {
    Console.WriteLine("generic " + Id(8) + " " + Id("s"));

    T Id<T>(T x) => x;
  }

  // Declared first: written as before.
  static void DeclaredFirst() {
    int Three() => 3;

    Console.WriteLine("first " + Three());
  }

  public static void Main() {
    Single();
    NamedAsGlobal();
    Recursive();
    Mutual();
    Through();
    CapturedAbove();
    CapturedBetween();
    InLambda();
    InIterator();
    Nested();
    Generic();
    DeclaredFirst();
  }
}
