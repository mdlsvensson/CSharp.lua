using System;

struct P {
  public int X, Y;
  public P(int x, int y) { X = x; Y = y; }
  public static bool operator ==(P a, P b) => a.X == b.X && a.Y == b.Y;
  public static bool operator !=(P a, P b) => !(a == b);
  public override bool Equals(object o) => o is P p && p == this;
  public override int GetHashCode() => X ^ Y;
}

class Program {
  static string Against(P? n) {
    return (n == null) + " " + (n != null) + " " + (null == n) + " " + (null != n) + " " + (n == default);
  }

  static string Nullables(P? a, P? b) {
    return (a == b) + " " + (a != b);
  }

  static string Mixed(P? a, P c) {
    return (a == c) + " " + (c != a);
  }

  public static void Main() {
    Console.WriteLine(Against(null));
    Console.WriteLine(Against(new P(1, 2)));
    Console.WriteLine(Nullables(null, null) + " " + Nullables(new P(1, 2), null) + " " + Nullables(null, new P(1, 2)));
    Console.WriteLine(Nullables(new P(1, 2), new P(1, 2)) + " " + Nullables(new P(1, 2), new P(3, 4)));
    Console.WriteLine(Mixed(null, new P(1, 2)) + " " + Mixed(new P(1, 2), new P(1, 2)));
    P plain = new P(5, 6);
#pragma warning disable CS0472
    Console.WriteLine((plain == null) + " " + (plain != null));
#pragma warning restore CS0472
    P? found = null;
    if (found == null) {
      found = new P(7, 8);
    }
    Console.WriteLine(found != null ? "found " + found.Value.X : "none");
  }
}
