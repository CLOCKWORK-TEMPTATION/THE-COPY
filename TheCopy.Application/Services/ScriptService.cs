
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCopy.Application.Interfaces;
using TheCopy.Application.Interfaces.AI;
using TheCopy.Domain.Entities;
using TheCopy.Shared.DataTransferObjects;

namespace TheCopy.Application.Services;

public class ScriptService: IScriptService
{
    private readonly IScriptRepository _scriptRepository;
    private readonly IGenerativeAiService _generativeAiService;

    public ScriptService(IScriptRepository scriptRepository, IGenerativeAiService generativeAiService)
    {
        _scriptRepository = scriptRepository;
        _generativeAiService = generativeAiService;
    }

    public async Task<Script> CreateScript(CreateScriptRequestDto model)
    {
        var content = await _generativeAiService.GenerateContent(model.Prompt);

        var script = new Script
        {
            Title = model.Title,
            Content = content,
            ProjectId = model.ProjectId
        };

        await _scriptRepository.AddAsync(script);

        return script;
    }

    public async Task<IEnumerable<Script>> GetScriptsByProject(Guid projectId)
    {
        return await _scriptRepository.GetByProjectIdAsync(projectId);
    }
}
