using System;

enum Kind { Below = -1, Zero = 0, Above = 1 }

class Program {
  const int Floor = -300;
  const long Far = -70000;
  const long Least = long.MinValue;
  const float Half = -0.5f;
  const double Third = -1.25;
  const char Letter = 'a';

  static int Default(int x = -1, long y = -2) {
    return x + (int)y;
  }

  public static void Main() {
    const int local = -9;
    int a = Floor;
    long b = Far;
    Kind k = Kind.Below;
    int c = (int)Kind.Below;
    Console.WriteLine(a + " " + b + " " + (int)k + " " + c);
    Console.WriteLine(int.MinValue + " " + (Floor * 2) + " " + local + " " + Default());
    long e = Least;
    Console.WriteLine(e < b);
    Console.WriteLine(Half + " " + Third + " " + (int)Letter);
    Console.WriteLine(Kind.Above.ToString() + " " + k.ToString());
  }
}
