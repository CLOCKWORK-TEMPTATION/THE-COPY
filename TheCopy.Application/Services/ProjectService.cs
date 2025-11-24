
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCopy.Application.Interfaces;
using TheCopy.Domain.Entities;
using TheCopy.Shared.DataTransferObjects;

namespace TheCopy.Application.Services;

public class ProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto> CreateProject(CreateProjectRequestDto model)
    {
        var project = new Project
        {
            Name = model.Name,
            UserId = model.UserId
        };

        await _projectRepository.AddAsync(project);

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            UserId = project.UserId
        };
    }

    public async Task<IEnumerable<ProjectDto>> GetProjectsByUser(Guid userId)
    {
        var projects = await _projectRepository.GetByUserIdAsync(userId);
        return projects.Select(p => new ProjectDto
        {
            Id = p.Id,
            Name = p.Name,
            UserId = p.UserId
        });
    }
}
