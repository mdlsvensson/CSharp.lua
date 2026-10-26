using System;

class Base {
  private int count;
  private int Total { get; set; }
  private event Action Changed;

  public void Add() {
    count++;
    Total += 10;
    Changed += () => Console.WriteLine("base changed");
  }

  public int Count => count;
  public int BaseTotal => Total;
  public void RaiseBase() => Changed?.Invoke();
  public int BaseTwice() => Twice();
  private int Twice() => 2;
}

class Derived : Base {
  private int count = 100;
  public int Total { get; set; } = 7;
  public event Action Changed;

  public int Own => count;
  public void RaiseOwn() => Changed?.Invoke();
  public int OwnTwice() => Twice();
  private int Twice() => 3;
}

class Public : Base {
  public int count = 200;
}

class Middle : Base {
}

class Deeper : Middle {
  private int count = 300;
  public int Own => count;
}

class Program {
  public static void Main() {
    var derived = new Derived();
    derived.Add();
    Console.WriteLine(derived.Count + " " + derived.Own);
    Console.WriteLine(derived.BaseTotal + " " + derived.Total);
    derived.Changed += () => Console.WriteLine("derived changed");
    derived.RaiseBase();
    derived.RaiseOwn();
    Console.WriteLine(derived.BaseTwice() + " " + derived.OwnTwice());
    var open = new Public();
    open.Add();
    Console.WriteLine(open.Count + " " + open.count);
    var deeper = new Deeper();
    deeper.Add();
    Console.WriteLine(deeper.Count + " " + deeper.Own);
  }
}
