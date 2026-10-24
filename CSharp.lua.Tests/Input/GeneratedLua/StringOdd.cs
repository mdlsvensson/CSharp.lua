class StringOdd {
  static string A(int n) => $"a\\{n}\"q\n\t{n}";
  static string B2(int n) => $@"100% {n} ""q"" \x";
  static string C(int n) => $"""100% {n} "q" \x""";
  static string D() => @"a""b\c";
  static string E() => "\r\n";
  static string F(int n) => $"{n,5}\u00e9";
  static char G(int n) => $"a{n}"[1];
  static int H(int n) => $"a{n}".Length;
}
