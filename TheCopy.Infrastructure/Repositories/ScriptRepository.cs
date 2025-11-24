
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheCopy.Application.Interfaces;
using TheCopy.Domain.Entities;
using TheCopy.Infrastructure.Data;

namespace TheCopy.Infrastructure.Repositories;

public class ScriptRepository : IScriptRepository
{
    private readonly ApplicationDbContext _context;

    public ScriptRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Script script)
    {
        _context.Scripts.Add(script);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Script>> GetByProjectIdAsync(Guid projectId)
    {
        return await _context.Scripts.Where(s => s.ProjectId == projectId).ToListAsync();
    }
}
