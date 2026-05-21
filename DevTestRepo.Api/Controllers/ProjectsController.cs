using DevTestRepo.Api.Data;
using DevTestRepo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTestRepo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProjectsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _context.Projects.ToListAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var project = await _context.Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project == null)
            return NotFound();

        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Project project)
    {
        if (string.IsNullOrWhiteSpace(project.Name))
            return BadRequest("Name is required.");

        project.CreatedAt = DateTime.UtcNow;
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Project incoming)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
            return NotFound();

        project.Name = incoming.Name;
        project.Description = incoming.Description;
        project.Status = incoming.Status;

        await _context.SaveChangesAsync();
        return Ok(project);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
            return NotFound();

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var projects = await _context.Projects
            .Where(p => p.Status == ProjectStatus.Active || p.Status == ProjectStatus.OnHold)
            .ToListAsync();

        return Ok(projects);
    }

    [HttpGet("{id}/summary")]
    public async Task<IActionResult> GetSummary(int id)
    {
        var project = await _context.Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project == null)
            return NotFound();

        var totalTasks = project.Tasks.Count;
        var completedTasks = project.Tasks.Count(t => t.IsComplete);
        var overdueTasks = project.Tasks.Count(t => !t.IsComplete && t.DueDate.HasValue && t.DueDate < DateTime.UtcNow);

        var summary = new
        {
            project.Id,
            project.Name,
            project.Status,
            TotalTasks = totalTasks,
            CompletedTasks = completedTasks,
            OverdueTasks = overdueTasks,
            CompletionPercent = totalTasks == 0 ? 0 : (completedTasks * 100) / totalTasks
        };

        return Ok(summary);
    }
}
