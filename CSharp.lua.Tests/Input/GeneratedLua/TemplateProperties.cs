using static Natives;

public static class Natives {
  /// @CSharpLua.Get = "ATTACK_TYPE_MAGIC"
  public static extern int ATTACK_TYPE_MAGIC { get; }
}

public static class Code {
  public static int F() => ATTACK_TYPE_MAGIC;
}
