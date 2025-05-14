using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project2
{
    class Program
    {
        static void Main()
        {
            string filePath = "data.txt";
            long fn = 22251421008;
            int n = (int)(fn % 4 + 2);
            Console.WriteLine($"n = {n}");

            int targetLastDigit = int.Parse(fn.ToString()[fn.ToString().Length - 2].ToString());

            List<MyTuple> allTuples = new List<MyTuple>();

            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] parts = line.Split(',');

                if (parts.Length == n)
                {
                    int val1 = int.Parse(parts[0].Trim());
                    int val2 = int.Parse(parts[1].Trim());
                    allTuples.Add(new MyTuple(val1, val2));
                }
            }

            var matching = allTuples.Where(t => t.Value2 % 10 == targetLastDigit);

            Console.WriteLine("Matching tuples:");
            foreach (var t in matching)
            {
                Console.WriteLine(t);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
