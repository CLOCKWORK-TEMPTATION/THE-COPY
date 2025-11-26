
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCopy.Application.Interfaces;
using TheCopy.Domain.Entities;
using TheCopy.Infrastructure.Data;

namespace TheCopy.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Project> GetByIdAsync(Guid id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<IEnumerable<Project>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Projects.Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task AddAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
    }

    public void Remove(Project project)
    {
        _context.Projects.Remove(project);
    }
}
