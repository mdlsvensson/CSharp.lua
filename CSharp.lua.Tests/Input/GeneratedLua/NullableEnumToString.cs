enum Color {
  Red,
  Green,
}

class Paint {
  public Color? Tint { get; set; }
}

class NullableEnumToString {
  static string Name(Color? c) {
    return c?.ToString();
  }

  static string Direct(Color? c) {
    return c.ToString();
  }

  static string Through(Paint paint) {
    return paint?.Tint?.ToString();
  }

  static int? Length(Color? c) {
    return c?.ToString().Length;
  }
}
