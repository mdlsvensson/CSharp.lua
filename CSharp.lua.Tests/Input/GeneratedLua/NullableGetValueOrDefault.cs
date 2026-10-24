struct Point {
  public int X;
}

class Holder {
  public int? Count { get; set; }
}

class NullableGetValueOrDefault {
  static int Count(int? n) {
    return n.GetValueOrDefault();
  }

  static bool Flag(bool? b) {
    return b.GetValueOrDefault();
  }

  static float Ratio(float? f) {
    return f.GetValueOrDefault();
  }

  static Point Origin(Point? p) {
    return p.GetValueOrDefault();
  }

  static int Fallback(int? n) {
    return n.GetValueOrDefault(7);
  }

  static int Through(Holder holder) {
    return holder?.Count.GetValueOrDefault() ?? -1;
  }
}
