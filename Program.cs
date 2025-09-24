using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("== Caso 1: OrderRange ==");

        var r1 = OrderRange.build(new[] { 2, 1, 4, 5 });
        Console.WriteLine(OrderRange.FormatResult(r1)); // [1, 5] [2, 4]

        var r2 = OrderRange.build(new[] { 4, 2, 9, 3, 6 });
        Console.WriteLine(OrderRange.FormatResult(r2)); // [3, 9] [2, 4, 6]

        var r3 = OrderRange.build(new[] { 58, 60, 55, 48, 57, 73 });
        Console.WriteLine(OrderRange.FormatResult(r3)); // [55, 57, 73] [48, 58, 60]

        Console.WriteLine();
        Console.WriteLine("== Caso 2: MoneyParts ==");

        var combos010 = MoneyParts.build("0.10");
        Console.WriteLine($"Combinaciones para 0.10: {combos010.Count}");
        foreach (var combo in combos010)
            Console.WriteLine($"[{string.Join(", ", combo)}]");
    }
}
