using System;
using System.Collections.Generic;
using System.Linq;

public static class OrderRange
{
    /// Recibe una colección de enteros positivos y retorna una lista de impares ascendentes y de pares ascendente
    public static (List<int> odds, List<int> evens) build(IEnumerable<int> numbers)
    {
        if (numbers is null)
            throw new ArgumentNullException(nameof(numbers));

        if (numbers.Any(n => n <= 0))
            throw new ArgumentException("Todos los números deben ser enteros positivos (> 0).", nameof(numbers));

        var odds  = numbers.Where(n => (n & 1) == 1).OrderBy(n => n).ToList();
        var evens = numbers.Where(n => (n & 1) == 0).OrderBy(n => n).ToList();

        return (odds, evens);
    }

    public static string FormatResult((List<int> odds, List<int> evens) result)
        => $"[{string.Join(", ", result.odds)}] [{string.Join(", ", result.evens)}]";
}
