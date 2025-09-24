using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public static class MoneyParts
{
    // Denominaciones en centavos: 200, 100, 50, 20, 10, 5, 2, 1, 0.5, 0.2, 0.1, 0.05
    private static readonly int[] DenominationsCents = new[]
    {
        20000, 10000, 5000, 2000, 1000, 500, 200, 100, 50, 20, 10, 5
    };

    /// Recibe un monto como cadena (ej. "10.50") y devuelve todas las combinaciones que suman el monto exacto
    public static List<List<decimal>> build(string amountStr)
    {
        if (string.IsNullOrWhiteSpace(amountStr))
            throw new ArgumentException("El monto no puede estar vacío.", nameof(amountStr));

        if (!TryParseAmount(amountStr, out var amount))
            throw new FormatException("Formato de monto inválido. Usa valores como 0.10, 10.50, etc.");

        if (amount < 0m)
            throw new ArgumentException("El monto no puede ser negativo.", nameof(amountStr));

        var amountCents = DecimalToCents(amount);

        var resultsCents = new List<List<int>>();
        var current = new List<int>();

        FindCombinations(amountCents, 0, current, resultsCents);

        var results = resultsCents
            .Select(combo => combo.Select(CentsToDecimal).ToList())
            .ToList();

        return results;
    }

    private static void FindCombinations(int remaining, int startIdx, List<int> current, List<List<int>> results)
    {
        if (remaining == 0)
        {
            results.Add(new List<int>(current));
            return;
        }

        for (int i = startIdx; i < DenominationsCents.Length; i++)
        {
            int coin = DenominationsCents[i];
            if (coin > remaining) continue;

            current.Add(coin);
            FindCombinations(remaining - coin, i, current, results);
            current.RemoveAt(current.Count - 1);
        }
    }

    private static bool TryParseAmount(string input, out decimal amount)
    {
        if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out amount))
            return true;

        var esPE = new CultureInfo("es-PE");
        return decimal.TryParse(input, NumberStyles.Number, esPE, out amount);
    }

    private static int DecimalToCents(decimal value)
        => (int)Math.Round(value * 100m, MidpointRounding.AwayFromZero);

    private static decimal CentsToDecimal(int cents)
        => Math.Round(cents / 100m, 2, MidpointRounding.AwayFromZero);
}
