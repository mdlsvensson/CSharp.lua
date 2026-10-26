using System;
enum E { Neg = -1, Pos = 2 }
static class K { public const float M = -3.5f; public const double D = -0.25; }
class Program {
  const float F = -2f;
  const int N = -5;
  const long L = -7;
  static void Main() {
    int x = 4;
    float f = 1.5f;
    var a = -F;
    Console.WriteLine(a);
    var b = -K.M;
    Console.WriteLine(b);
    var c = -(int)E.Neg;
    Console.WriteLine(c);
    var d = -N;
    Console.WriteLine(d);
    var e = -(N);
    Console.WriteLine(e);
    Console.WriteLine($"{-N}");
    var g = - -x;
    Console.WriteLine(g);
    var h = -(float)-x;
    Console.WriteLine(h);
    var i = -L * 2;
    Console.WriteLine(i);
    var j = -K.D;
    Console.WriteLine(j);
    var k = - -f;
    Console.WriteLine(k);
    var m = 3 - -N;
    Console.WriteLine(m);
    const int local = -6;
    var n = -local;
    Console.WriteLine(n);
    var o = - - -x;
    Console.WriteLine(o);
    Console.WriteLine("after");
  }
}
