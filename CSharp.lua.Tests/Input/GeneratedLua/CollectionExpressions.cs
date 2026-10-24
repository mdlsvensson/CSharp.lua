using System.Collections.Generic;

class CollectionExpressions {
  public static int[] Array() => [1, 2, 3];

  static List<int> field = [1, 2];

  public static List<int> Spread(List<int> a) => [..a, 2];

  public static void Local() {
    List<int> a = [1];
    int[] b = [];
    a = [4, 5];
    Take([7, 8]);
  }

  static void Take(IEnumerable<int> items) {
  }
}
