using DevTestRepo.Api.Models;

namespace DevTestRepo.Api.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Projects.Any())
            return;

        var alpha = new Project
        {
            Name = "Project Alpha",
            Description = "Internal tooling rewrite for the ops team.",
            Status = ProjectStatus.Active,
            CreatedAt = DateTime.UtcNow.AddDays(-60)
        };

        var beta = new Project
        {
            Name = "Project Beta",
            Description = "Customer-facing portal redesign.",
            Status = ProjectStatus.Active,
            CreatedAt = DateTime.UtcNow.AddDays(-30)
        };

        var legacy = new Project
        {
            Name = "Legacy Cleanup",
            Description = "Decommissioning old payment gateway integration.",
            Status = ProjectStatus.OnHold,
            CreatedAt = DateTime.UtcNow.AddDays(-90)
        };

        context.Projects.AddRange(alpha, beta, legacy);
        context.SaveChanges();

        context.Tasks.AddRange(
            new TaskItem { Title = "Set up CI pipeline", ProjectId = alpha.Id, Priority = Priority.High, IsComplete = true, DueDate = DateTime.UtcNow.AddDays(-45) },
            new TaskItem { Title = "Write deployment runbook", ProjectId = alpha.Id, Priority = Priority.Medium, IsComplete = false, DueDate = DateTime.UtcNow.AddDays(10) },
            new TaskItem { Title = "Migrate user configs", ProjectId = alpha.Id, Priority = Priority.High, IsComplete = false, DueDate = DateTime.UtcNow.AddDays(5) },
            new TaskItem { Title = "Design system audit", ProjectId = beta.Id, Priority = Priority.Medium, IsComplete = true, DueDate = DateTime.UtcNow.AddDays(-20) },
            new TaskItem { Title = "Implement dark mode", ProjectId = beta.Id, Priority = Priority.Low, IsComplete = false },
            new TaskItem { Title = "Accessibility review", ProjectId = beta.Id, Priority = Priority.High, IsComplete = false, DueDate = DateTime.UtcNow.AddDays(3) },
            new TaskItem { Title = "Identify dependent services", ProjectId = legacy.Id, Priority = Priority.High, IsComplete = false, DueDate = DateTime.UtcNow.AddDays(-5) },
            new TaskItem { Title = "Archive transaction logs", ProjectId = legacy.Id, Priority = Priority.Low, IsComplete = false }
        );

        context.SaveChanges();
    }
}
