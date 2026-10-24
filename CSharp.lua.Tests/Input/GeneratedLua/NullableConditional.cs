class Holder {
  public int Count;
}

struct Wrap {
  public int V;
}

class NullableConditional {
  static int? Pick(bool c, int? n, int? x) {
    return c ? n : x;
  }

  static bool? PickBool(bool c, bool? b, bool? x) {
    return c ? b : x;
  }

  static int? NullElse(bool c, int? n) {
    return c ? n : null;
  }

  static int? Checked(int? n, int? x) {
    return n != null ? n : x;
  }

  static int? FromPlain(bool c, int i, int? x) {
    return c ? (int?)i : x;
  }

  static string Reference(bool c, string s, string x) {
    return c ? s : x;
  }

  static int Plain(bool c, int a, int b) {
    return c ? a : b;
  }

  static bool PlainBool(bool c, bool a, bool b) {
    return c ? a : b;
  }

  static Wrap? Struct(bool c, Wrap? w, Wrap? x) {
    return c ? w : x;
  }

  static T Generic<T>(bool c, T a, T b) {
    return c ? a : b;
  }

  static int? Property(bool c, Holder h, int? x) {
    return c ? h?.Count : x;
  }
}

class Constrained {
  static T Pick<T>(bool c, T a, T b) where T : struct {
    return c ? a : b;
  }
}
