using System;
using System.Collections.Generic;
using System.Linq;

class Program {
  static IEnumerable<int> Numbers(string name) {
    try {
      yield return 1;
      yield return 2;
      yield return 3;
    } finally {
      Console.WriteLine("finally " + name);
    }
  }

  static int FirstOver(int limit) {
    foreach (var n in Numbers("return")) {
      if (n > limit) {
        return n;
      }
    }
    return -1;
  }

  static IEnumerable<int> Nested() {
    try {
      foreach (var n in Numbers("inner")) {
        yield return n * 10;
        if (n == 2) {
          yield break;
        }
      }
    } finally {
      Console.WriteLine("finally outer");
    }
  }

  // A list's enumerator needs no Dispose: the loop is written as before.
  static int ListLoop(List<int> list) {
    foreach (var n in list) {
      if (n > 1) {
        return n;
      }
    }
    return -1;
  }

  public static void Main() {
    foreach (var n in Numbers("break")) {
      Console.WriteLine(n);
      break;
    }
    Console.WriteLine(FirstOver(1));
    foreach (var n in Numbers("whole")) {
    }
    foreach (var n in Numbers("where").Where(i => i > 1)) {
      Console.WriteLine(n);
      break;
    }
    foreach (var n in Nested()) {
      Console.WriteLine(n);
    }
    using (var e = Numbers("using").GetEnumerator()) {
      e.MoveNext();
      Console.WriteLine(e.Current);
    }
    foreach (var n in Numbers("switch")) {
      switch (n) {
        case 1:
          Console.WriteLine("one");
          break;
        default:
          break;
      }
      Console.WriteLine("after switch " + n);
      if (n == 2) {
        break;
      }
    }
    Console.WriteLine(ListLoop(new List<int> { 1, 2, 3 }));
  }
}
