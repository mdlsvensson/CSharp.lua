using System;
using System.Collections.Generic;

class Program {
  static bool go = true;
  static readonly List<int> entries = new List<int> { 0, 1, 2 };
  static int next;

  // A loop with a continue whose last statement is a return with a comment after it: Moonwell's line marks put a code
  // template there, and a // comment is the same.
  static void Advance() {
    while (next < entries.Count) {
      var entry = entries[next];
      next++;
      if (entry == 0) {
        Console.WriteLine("measure");
        continue;
      }
      if (entry == 1) {
        Console.WriteLine("pause");
        return; /*[[--@mw 1:21]]*/
      }
      Console.WriteLine("named");
      return; /*[[--@mw 1:24]]*/
    }
  }

  static int LineComment() {
    foreach (var x in entries) {
      if (x == 0) {
        continue;
      }
      return x; // the first that is not 0
    }
    return -1;
  }

  // A for loop the compiler writes as a while, with its step after the body.
  static int Stepped() {
    for (int i = 0; i < 9 && go; i += 2) {
      if (i == 0) {
        continue;
      }
      return i;
    }
    return -1;
  }

  static int SteppedNoContinue() {
    for (int i = 1; i < 9 && go; i += 2) {
      return i;
    }
    return -1;
  }

#pragma warning disable CS0162
  // Statements after a return that can never run.
  static int AfterReturnInLoop() {
    for (int i = 0; i < 5; i++) {
      if (i < 3) {
        continue;
      }
      return i;
      Console.WriteLine("never");
    }
    return -1;
  }

  static int AfterReturnInIf(int x) {
    if (x > 0) {
      return 1;
      Console.WriteLine("never");
    }
    return 0;
  }

  static int AfterReturnInSwitch(int x) {
    switch (x) {
      case 1:
        return 10;
        Console.WriteLine("never");
      default:
        return 0;
    }
  }

  static int AfterReturnInMethod() {
    return 7;
    Console.WriteLine("never");
  }
#pragma warning restore CS0162

  public static void Main() {
    Advance();
    Advance();
    Advance();
    Console.WriteLine(LineComment());
    Console.WriteLine(Stepped());
    Console.WriteLine(SteppedNoContinue());
    Console.WriteLine(AfterReturnInLoop());
    Console.WriteLine(AfterReturnInIf(1));
    Console.WriteLine(AfterReturnInSwitch(1));
    Console.WriteLine(AfterReturnInMethod());
#pragma warning disable CS0162
    Func<int> lambda = () => {
      for (int i = 0; i < 3; i++) {
        if (i == 0) {
          continue;
        }
        return i; /*[[--@mw 1:110]]*/
      }
      return -1;
    };
    Action after = () => {
      Console.WriteLine("lambda");
      return;
      Console.WriteLine("never");
    };
#pragma warning restore CS0162
    Console.WriteLine(lambda());
    after();
  }
}
