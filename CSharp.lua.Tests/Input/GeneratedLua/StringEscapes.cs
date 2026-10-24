class StringEscapes {
  static string Escape() => "a\eb";
  static string Wide() => "a\U0001F600b";
  static string Short() => "a\x1b";
  static string ShortThenText() => "a\x1bc";
  static string ShortThenHex() => "a\x41g";
  static string Nul() => "a\0b";
  static string NulThenDigit() => "a\01";
  static string Quote() => "say \"hi\" \\ \u00e9 \t";
  static string Raw() => """
    raw "text"
    """;
  static string Utf8() => "abc"u8.Length.ToString();
  static string Verbatim() => @"
line";
  static string Interp(int n) => $"a\e{n}\x1b";
  static string InterpVerbatim(int n) => $@"
{n} \e";
  static string InterpRaw(int n) => $"""v {n} \e""";
  const string Reset = "\x1b[0m";
  static string Const() => Reset;
  static string JoinedEscape() => "\e" + "y";
  static string JoinedNul() => "b\0" + "2";
}
