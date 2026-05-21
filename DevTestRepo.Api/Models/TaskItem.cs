namespace DevTestRepo.Api.Models;

public enum Priority
{
    Low,
    Medium,
    High
}

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsComplete { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public DateTime? DueDate { get; set; }
    public int ProjectId { get; set; }

    public Project? Project { get; set; }
}
