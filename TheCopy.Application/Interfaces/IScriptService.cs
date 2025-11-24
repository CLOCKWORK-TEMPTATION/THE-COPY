using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCopy.Domain.Entities;
using TheCopy.Shared.DataTransferObjects;

namespace TheCopy.Application.Interfaces
{
    public interface IScriptService
    {
        Task<Script> CreateScript(CreateScriptRequestDto model);
        Task<IEnumerable<Script>> GetScriptsByProject(Guid projectId);
    }
}
