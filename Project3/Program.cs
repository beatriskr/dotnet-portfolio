using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        KanbanBoard board;
        string filePath;

        Console.WriteLine("KANBAN BOARD");
        Console.WriteLine("1. Start new board");
        Console.WriteLine("2. Load existing board");
        Console.Write("Enter option: ");
        var input = Console.ReadLine();

        if (input == "1")
        {
            Console.Write("Enter project name: ");
            string name = Console.ReadLine();
            Console.Write("Enter filename to save (e.g., kanban.json): ");
            filePath = Console.ReadLine();

            board = new KanbanBoard { ProjectName = name };
        }
        else
        {
            Console.Write("Enter filename to load: ");
            filePath = Console.ReadLine();

            string json = File.ReadAllText(filePath);
            board = JsonSerializer.Deserialize<KanbanBoard>(json);
        }

        bool running = true;
        while (running)
        {
            DrawBoard(board, filePath);

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.F2:
                    AddTask(board);
                    break;
                case ConsoleKey.F5:
                    MoveTask(board.ToDo, board.InProgress, "To Do", "In Progress");
                    break;
                case ConsoleKey.F6:
                    MoveTask(board.InProgress, board.Done, "In Progress", "Done");
                    break;
                case ConsoleKey.F9:
                    SaveBoard(board, filePath);
                    running = false;
                    break;
            }
        }
    }

    static void DrawBoard(KanbanBoard board, string filePath)
    {
        Console.Clear();

        Console.WriteLine($"KANBAN BOARD : {board.ProjectName}   ({filePath})");
        Console.WriteLine();
        Console.WriteLine($"| To Do ({board.ToDo.Count}/10)       | In Progress ({board.InProgress.Count}/10) | Done ({board.Done.Count}/10)       |");
        Console.WriteLine("--------------------------------------------------------------------------");

        for (int i = 0; i < 10; i++)
        {
            string todo = i < board.ToDo.Count ? $"{i + 1}. {board.ToDo[i].Title}" : "";
            string prog = i < board.InProgress.Count ? $"{i + 1}. {board.InProgress[i].Title}" : "";
            string done = i < board.Done.Count ? $"{i + 1}. {board.Done[i].Title}" : "";

            Console.WriteLine($"| {todo.PadRight(20)}| {prog.PadRight(20)}| {done.PadRight(20)}|");
        }

        Console.WriteLine("--------------------------------------------------------------------------");
        Console.WriteLine("[F1] Task details   [F2] Add task   [F5] Set in progress   [F6] Set as done   [F9] End");
        Console.WriteLine();
    }

    static void AddTask(KanbanBoard board)
    {
        if (board.ToDo.Count >= 10)
        {
            Console.WriteLine("❌ Cannot add more tasks to To Do (max 10)");
            Console.ReadKey();
            return;
        }

        Console.Write("New Task Title: ");
        string title = Console.ReadLine();
        Console.Write("Description: ");
        string description = Console.ReadLine();

        int newId = board.ToDo.Count + board.InProgress.Count + board.Done.Count + 1;

        board.ToDo.Add(new TaskItem
        {
            Id = newId,
            Title = title,
            Description = description
        });
    }

    static void MoveTask(List<TaskItem> from, List<TaskItem> to, string fromName, string toName)
    {
        if (from.Count == 0)
        {
            Console.WriteLine($"❌ No tasks in {fromName} to move.");
            Console.ReadKey();
            return;
        }

        if (to.Count >= 10)
        {
            Console.WriteLine($"❌ Cannot move to {toName} (max 10 tasks).");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"\nSelect task number from {fromName} to move to {toName}:");

        for (int i = 0; i < from.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {from[i].Title}");
        }

        Console.Write("Enter number: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= from.Count)
        {
            var task = from[index - 1];
            from.RemoveAt(index - 1);
            to.Add(task);
        }
        else
        {
            Console.WriteLine("❌ Invalid input.");
            Console.ReadKey();
        }
    }

    static void SaveBoard(KanbanBoard board, string filePath)
    {
        string json = JsonSerializer.Serialize(board, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
        Console.WriteLine("✅ Board saved successfully.");
        Console.ReadKey();
    }
}
