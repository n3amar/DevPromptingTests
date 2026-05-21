using DevTestRepo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DevTestRepo.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Project>()
            .Property(p => p.Status)
            .HasConversion<string>();

        modelBuilder.Entity<TaskItem>()
            .Property(t => t.Priority)
            .HasConversion<string>();
    }
}
