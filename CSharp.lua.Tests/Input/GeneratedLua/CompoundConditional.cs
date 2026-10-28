using System;
using System.Collections.Generic;
class Program {
  static int[] numbers = new int[3];
  static int count;
  int value;
  public int Value { get { return value; } set { this.value = value; } }
  public static int Count { get { return count; } set { count = value; } }
  static void Main() {
    var yes = true;
    var no = false;
    numbers[0] = 5;
    numbers[0] += yes ? 1 : 0;
    Console.WriteLine(numbers[0]);
    numbers[1] = 5;
    numbers[1] -= no ? 1 : 2;
    Console.WriteLine(numbers[1]);
    var list = new List<int> { 5 };
    list[0] *= yes ? 2 : 3;
    Console.WriteLine(list[0]);
    var map = new Dictionary<int, int> { [1] = 5 };
    map[1] += yes ? 1 : 0;
    Console.WriteLine(map[1]);
    var grid = new int[2, 2];
    grid[1, 1] += yes ? 4 : 0;
    Console.WriteLine(grid[1, 1]);
    var p = new Program { Value = 5 };
    p.Value += yes ? 1 : 0;
    Console.WriteLine(p.Value);
    Count = 5;
    Count += no ? 1 : 3;
    Console.WriteLine(Count);
    numbers[2] = 5;
    var result = numbers[2] += yes ? 1 : 0;
    Console.WriteLine(result);
    var words = new string[] { "a" };
    words[0] += yes ? "x" : "y";
    Console.WriteLine(words[0]);
    var local = 5;
    local += yes ? 1 : 0;
    Console.WriteLine(local);
    numbers[0] = 10;
    numbers[0] -= 3 - 2;
    Console.WriteLine(numbers[0]);
  }
}
