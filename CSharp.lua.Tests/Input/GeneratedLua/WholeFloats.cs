using System;

struct Point {
  public float X;
  public int N;
}

class WholeFloats {
  const float K = 5f;
  const double D = 2d;
  float field;
  float[] items = new float[3];
  Point point;
  static readonly float Big = 16777216f + 1f;

  static float Literal() => 1f;
  static float LiteralDouble() => 100d.GetHashCode() + 3.0f;
  static float Folded() => 16777216f + 1f;
  static float Const() => K * 2;
  static float Cast(int count) => (float)count;
  static float Implicit(int count) {
    float f = count;
    return f;
  }
  static float Stored(int count) {
    var f = 0f;
    f = count;
    f += count;
    return f;
  }
  static float Arith(int a, int b) => (float)a * b + 0.5f;
  static float Div(int a, int b) => (float)a / b;
  static float Mixed(float f, int n) => f * n;
  static float Default(float step = 2) => step;
  static float Call() => Default() + Default(3);
  static float Floor(float f) => (float)Math.Floor(f);
  static float Ceil(float f) => (float)Math.Ceiling(f);
  static float? Maybe(int? n) => n;
  static double AsDouble(int n) => n;
  static int Back(float f) => (int)f;
  float Field() => field + point.X;
  float Item() => items[0];
  static float Neg() => -1f;
  static float Zero() => 0f;
  static string Text() => "x" + 4f + 1.5f;
  static float[] Array() => new float[] { 1f, 2f };
  static float Sum(params float[] values) => values[0];
  static float PassInt() => Sum(1, 2);
}
