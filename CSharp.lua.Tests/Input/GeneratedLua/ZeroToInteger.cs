using System;

class Program {
  // Read at run time, so the compiler does not fold the conversions.
  static float zero = 0f;
  static double zeroDouble = 0.0;

  static float Float(float f) => f;

  public static void Main() {
    Console.WriteLine("int 0f " + (int)zero);
    Console.WriteLine("int -0f " + (int)(-zero));
    Console.WriteLine("int 0.5f " + (int)Float(0.5f));
    Console.WriteLine("int -0.5f " + (int)Float(-0.5f));
    Console.WriteLine("int 1f " + (int)Float(1f));
    Console.WriteLine("int -1.5f " + (int)Float(-1.5f));
    Console.WriteLine("int 0.0 " + (int)zeroDouble);
    Console.WriteLine("long 0.0 " + (long)zeroDouble);
    Console.WriteLine("Floor " + (int)MathF.Floor(zero));
    Console.WriteLine("Round " + (int)MathF.Round(zero));
    Console.WriteLine("Round -0.4f " + (int)MathF.Round(Float(-0.4f)));
    Console.WriteLine("Truncate " + (int)Math.Truncate(zeroDouble));
    Console.WriteLine("Ceiling " + (int)Math.Ceiling(zeroDouble));
    Console.WriteLine("Ceiling -0.0 " + (int)Math.Ceiling(-zeroDouble));
    Console.WriteLine("Ceiling -0.5 " + (int)Math.Ceiling((double)Float(-0.5f)));
    Console.WriteLine("Ceiling 0.5 " + (int)Math.Ceiling((double)Float(0.5f)));
    Console.WriteLine("MathF.Ceiling " + (int)MathF.Ceiling(zero));
    Console.WriteLine("Round up " + (int)Math.Round(zeroDouble, MidpointRounding.ToPositiveInfinity));
    Console.WriteLine("Round to zero " + (int)Math.Round(-zeroDouble, MidpointRounding.ToZero));
    Console.WriteLine("Convert " + Convert.ToInt32(zeroDouble));
    Console.WriteLine("Divide " + (int)(zero / 3f));
  }
}
