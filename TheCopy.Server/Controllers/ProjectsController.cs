
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheCopy.Application.Interfaces;
using TheCopy.Domain.Entities;
using TheCopy.Shared.DataTransferObjects;
using TheCopy.Infrastructure.Data; // Required for SaveChangesAsync

namespace TheCopy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;
    private readonly ApplicationDbContext _context; // Temporary, for unit of work

    public ProjectsController(IProjectRepository projectRepository, ApplicationDbContext context)
    {
        _projectRepository = projectRepository;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _projectRepository.GetAllAsync();
        var projectDtos = projects.Select(p => new ProjectDto
        {
            Id = p.Id,
            Name = p.Name,
            CreatedAt = p.CreatedAt
        }).ToList();

        return Ok(projectDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        var projectDto = new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            CreatedAt = project.CreatedAt
        };

        return Ok(projectDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectDto projectDto)
    {
        var project = new Project
        {
            Name = projectDto.Name,
            CreatedAt = DateTime.UtcNow
        };

        await _projectRepository.AddAsync(project);
        await _context.SaveChangesAsync();

        var newProjectDto = new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            CreatedAt = project.CreatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = project.Id }, newProjectDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        _projectRepository.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
