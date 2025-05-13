using System;
using System.Collections.Generic;

class Program
{
    static List<string> tasks = new List<string>();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("📝 TO-DO СПИСЪК");
            Console.WriteLine("--------------------");
            ShowTasks();

            Console.WriteLine("\nИзбери опция:");
            Console.WriteLine("1. Добави задача");
            Console.WriteLine("2. Премахни задача");
            Console.WriteLine("3. Изход");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    RemoveTask();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Невалиден избор. Натисни Enter.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void ShowTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("🔹 Няма добавени задачи.");
        }
        else
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i]}");
            }
        }
    }

    static void AddTask()
    {
        Console.Write("\nВъведи задача: ");
        var task = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(task))
        {
            tasks.Add(task);
            Console.WriteLine("✅ Задачата е добавена. Натисни Enter.");
        }
        else
        {
            Console.WriteLine("⚠️ Невалидна задача.");
        }
        Console.ReadLine();
    }

    static void RemoveTask()
    {
        Console.Write("\nВъведи номер на задача за изтриване: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= tasks.Count)
        {
            tasks.RemoveAt(index - 1);
            Console.WriteLine("🗑️ Задачата е премахната. Натисни Enter.");
        }
        else
        {
            Console.WriteLine("⚠️ Невалиден номер.");
        }
        Console.ReadLine();
    }
}
