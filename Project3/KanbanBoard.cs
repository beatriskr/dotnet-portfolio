using System.Collections.Generic;

public class KanbanBoard
{
    public string ProjectName { get; set; }
    public List<TaskItem> ToDo { get; set; } = new();
    public List<TaskItem> InProgress { get; set; } = new();
    public List<TaskItem> Done { get; set; } = new();
}
