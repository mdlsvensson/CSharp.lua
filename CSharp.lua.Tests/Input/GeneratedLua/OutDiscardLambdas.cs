using System;

static class Terrain {
  public static bool CanStand(float x, float y, out float z) {
    z = x + y;
    Console.WriteLine("stand " + x);
    return x >= 0f;
  }

  public static bool Two(out int a, out int b) {
    a = 1;
    b = 2;
    return true;
  }
}

class Point {
  public int X;
}

class Program {
  public static void Main() {
    // A lambda that returns nothing, with an expression body whose call has an out argument or an initializer.
    Action discard = () => Terrain.CanStand(1f, 2f, out _);
    discard();
    Action<float> declared = x => Terrain.CanStand(x, 3f, out var z);
    declared(2f);
    Action two = () => Terrain.Two(out _, out _);
    two();
    Action created = () => new Point { X = 4 };
    created();

    // The same calls in shapes that were already right.
    Func<bool> value = () => Terrain.CanStand(5f, 0f, out _);
    Console.WriteLine(value());
    Action block = () => { Terrain.CanStand(6f, 0f, out _); };
    block();
    Terrain.CanStand(7f, 0f, out _);
  }
}
