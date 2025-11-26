
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCopy.Domain.Entities;

namespace TheCopy.Application.Interfaces;

public interface IProjectRepository
{
    Task<Project> GetByIdAsync(Guid id);
    Task<IEnumerable<Project>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Project>> GetAllAsync();
    Task AddAsync(Project project);
    void Remove(Project project);
}
