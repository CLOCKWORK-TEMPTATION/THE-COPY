
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCopy.Domain.Entities;

namespace TheCopy.Application.Interfaces;

public interface IScriptRepository
{
    Task AddAsync(Script script);
    Task<IEnumerable<Script>> GetByProjectIdAsync(Guid projectId);
}
