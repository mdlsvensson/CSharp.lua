using System;
using System.Collections.Generic;

class Program {
  static void For() {
    for (int n = 1; n <= 3; n++) {
      switch (n) {
        case 2:
          continue;
        default:
          break;
      }
      Console.WriteLine("for " + n);
    }
  }

  static void Foreach() {
    foreach (var n in new List<int> { 1, 2, 3 }) {
      switch (n) {
        case 1:
          continue;
      }
      Console.WriteLine("foreach " + n);
    }
  }

  static void While() {
    int n = 0;
    while (n < 3) {
      n++;
      switch (n) {
        case 3:
          continue;
      }
      Console.WriteLine("while " + n);
    }
  }

  // A switch inside a switch: the continue leaves both.
  static void Nested() {
    for (int n = 1; n <= 2; n++) {
      switch (n) {
        case 1:
          switch (n + 1) {
            case 2:
              continue;
          }
          Console.WriteLine("never");
          break;
      }
      Console.WriteLine("nested " + n);
    }
  }

  // A switch inside a try inside the loop.
  static void InTry() {
    for (int n = 1; n <= 2; n++) {
      try {
        switch (n) {
          case 1:
            continue;
        }
        Console.WriteLine("try " + n);
      } finally {
        Console.WriteLine("finally " + n);
      }
    }
  }

  // A continue of a loop inside the switch is that loop's own.
  static void InnerLoop() {
    for (int n = 1; n <= 2; n++) {
      switch (n) {
        case 1:
          for (int i = 0; i < 2; i++) {
            if (i == 0) {
              continue;
            }
            Console.WriteLine("inner " + i);
          }
          break;
      }
      Console.WriteLine("outer " + n);
    }
  }

  public static void Main() {
    For();
    Foreach();
    While();
    Nested();
    InTry();
    InnerLoop();
  }
}
