
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TheCopy.Application.Services;
using TheCopy.Shared.DataTransferObjects;

namespace TheCopy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectController : ControllerBase
{
    private readonly ProjectService _projectService;

    public ProjectController(ProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectRequestDto model)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());
        var project = await _projectService.CreateProjectAsync(model, userId);
        return Ok(project);
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());
        var projects = await _projectService.GetAllProjectsAsync(userId);
        return Ok(projects);
    }
}
