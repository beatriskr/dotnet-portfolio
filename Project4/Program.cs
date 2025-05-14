using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.Write("Enter input file name (e.g., input.txt): ");
        string inputFile = Console.ReadLine() ?? "";

        if (!File.Exists(inputFile))
        {
            Console.WriteLine("❌ File not found.");
            return;
        }

        string text = File.ReadAllText(inputFile);

        // Извличане на числа
        var matches = Regex.Matches(text, @"\b\d+\b");
        var numbers = matches
            .Select(m => int.Parse(m.Value))
            .Where(n => n >= 1 && n <= 120)
            .ToList();

        if (numbers.Count == 0)
        {
            Console.WriteLine("⚠️ No valid numbers found.");
            return;
        }

        // Честота
        var freq = numbers
            .GroupBy(n => n)
            .OrderBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Count());

        // Заглавие за табличния изглед
        Console.WriteLine();
        Console.WriteLine("Число | Брой | Графика");
        Console.WriteLine("------+------|----------------");

        // Запис в .chart и печат в терминала
        string outputFile = inputFile + ".chart";
        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            foreach (var entry in freq)
            {
                string line = $"{entry.Key}: {new string('*', entry.Value)}";
                writer.WriteLine(line);

                Console.WriteLine($"{entry.Key,5} | {entry.Value,4} | {new string('*', entry.Value)}");
            }
        }

        Console.WriteLine($"\n✅ Chart saved to {outputFile}");
    }
}
