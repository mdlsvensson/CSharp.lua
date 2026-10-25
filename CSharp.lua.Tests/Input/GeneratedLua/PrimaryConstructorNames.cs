using System;

class Base(string n) {
  public string FromBase() => n;
}

class Derived(string n) : Base(n + "!") {
  public string FromDerived() => n;
}

class Deeper(string n) : Derived(n + "?") {
  public string FromDeeper() => n;
}

class WithField {
  public int count = 1;
}

// A parameter named as a base class's field: the field hides it in the class's methods (as C# reads it), and keeping the
// parameter does not change the field.
class FieldNamed(int count) : WithField {
  public int Kept() => count;
}

class Program {
  public static void Main() {
    var derived = new Derived("q");
    Console.WriteLine(derived.FromBase() + " " + derived.FromDerived());
    var deeper = new Deeper("d");
    Console.WriteLine(deeper.FromBase() + " " + deeper.FromDerived() + " " + deeper.FromDeeper());
    var named = new FieldNamed(5);
    Console.WriteLine(named.count + " " + named.Kept());
  }
}
