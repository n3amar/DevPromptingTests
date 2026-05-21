using DevTestRepo.Api.Data;
using DevTestRepo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTestRepo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _context.Tasks.ToListAsync();

        var result = new List<object>();
        foreach (var task in tasks)
        {
            var project = await _context.Projects.FindAsync(task.ProjectId);
            result.Add(new
            {
                task.Id,
                task.Title,
                task.IsComplete,
                task.Priority,
                task.DueDate,
                ProjectName = project?.Name
            });
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _context.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpGet("by-project/{projectId}")]
    public async Task<IActionResult> GetByProject(int projectId)
    {
        var projectExists = await _context.Projects.AnyAsync(p => p.Id == projectId);
        if (!projectExists)
            return NotFound("Project not found.");

        var tasks = await _context.Tasks
            .Where(t => t.ProjectId == projectId || t.IsComplete == false)
            .ToListAsync();

        return Ok(tasks);
    }

    [HttpGet("incomplete")]
    public async Task<IActionResult> GetIncomplete()
    {
        var tasks = await _context.Tasks
            .Include(t => t.Project)
            .Where(t => t.IsComplete == false)
            .OrderBy(t => t.DueDate)
            .ToListAsync();

        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
            return BadRequest("Title is required.");

        var projectExists = await _context.Projects.AnyAsync(p => p.Id == task.ProjectId);
        if (!projectExists)
            return BadRequest("Invalid project.");

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TaskItem incoming)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();

        task.Title = incoming.Title;
        task.Description = incoming.Description;
        task.IsComplete = incoming.IsComplete;
        task.Priority = incoming.Priority;
        task.DueDate = incoming.DueDate;

        await _context.SaveChangesAsync();
        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> MarkComplete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();

        task.IsComplete = true;
        await _context.SaveChangesAsync();

        return Ok(task);
    }
}
