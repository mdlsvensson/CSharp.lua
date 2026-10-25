using System;

class Field(int x) {
  public int X = x;
}

class Property(int x) {
  public int X { get; } = x;
}

struct Pair(int a, int b) {
  public int Sum = a + b;
}

class Base(string n) {
  public string N = n;

  public virtual string Show() => N;
}

class Derived(string n, int k) : Base(n) {
  public int K = k;
}

// A parameter used in a method body too, and a second constructor that calls the primary one.
class Kept(int x) {
  public int Twice = x * 2;

  public Kept() : this(10) {
  }

  public int Get() => x;
}

// The base constructor calls a virtual method that reads a parameter the derived class keeps.
class Early(int x) : Base("early") {
  public override string Show() => "x=" + x;
}

class Program {
  public static void Main() {
    Console.WriteLine(new Field(5).X);
    Console.WriteLine(new Property(6).X);
    Console.WriteLine(new Pair(3, 4).Sum);
    var derived = new Derived("nn", 3);
    Console.WriteLine(derived.N + derived.K);
    var kept = new Kept(7);
    Console.WriteLine(kept.Twice + " " + kept.Get());
    var chained = new Kept();
    Console.WriteLine(chained.Twice + " " + chained.Get());
    Console.WriteLine(new Early(8).Show());
  }
}
