struct Spot : System.IFormattable {
  public int Value;
  public override string ToString() => "spot " + Value;
  public string ToString(string format, System.IFormatProvider provider) => ToString() + " as " + format;
}

class Bag {
  public int Count;
  public bool On;
}

class NullableText {
  static string Concat(int? n, float? f, bool? b, char? c, Spot? s) {
    return "n=" + n + " f=" + f + " b=" + b + " c=" + c + " s=" + s;
  }

  static string Before(int? n) {
    return n + "!";
  }

  static string Through(Bag bag) {
    return "count " + bag?.Count + " on " + bag?.On;
  }

  static string Compound(int? n) {
    var text = "s=";
    text += n;
    return text;
  }

  static string Operands(int? n, int i) {
    return "x" + (n + 1) + (int?)i;
  }

  static string Interpolated(int? n, float? f, Spot? s) {
    return $"[{n}|{f}|{s}]";
  }

  static string Formatted(int? n, float? f) {
    return $"[{n:D3}|{f:F1}]";
  }

  static string Width(int? n) {
    return $"[{n,3}]";
  }

  static string Plain(int n, float f, bool b) {
    return "n=" + n + " f=" + f + " b=" + b + n.ToString();
  }

  static string Safe(int? n) {
    return n.ToString() + n?.ToString();
  }

  static string StructFormat(Spot? s, Spot t) {
    return $"[{s:Q}|{t:Q}]";
  }

  static string CharFormat(char? c, char d) {
    return $"[{c}|{c:X}|{d}]" + c;
  }
}
