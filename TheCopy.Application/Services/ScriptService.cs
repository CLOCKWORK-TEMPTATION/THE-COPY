using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCopy.Application.Interfaces;
using TheCopy.Domain.Entities;
using TheCopy.Shared.DataTransferObjects;

namespace TheCopy.Application.Services;

public class ScriptService : IScriptService
{
    private readonly IScriptRepository _scriptRepository;

    public ScriptService(IScriptRepository scriptRepository)
    {
        _scriptRepository = scriptRepository;
    }

    public async Task<GeneratedScriptDto> CreateScriptAsync(CreateScriptRequestDto request)
    {
        var script = new Script
        {
            Title = request.Title,
            Content = request.Content,
            ProjectId = request.ProjectId
        };

        await _scriptRepository.AddAsync(script);

        return new GeneratedScriptDto
        {
            Title = script.Title,
            Content = script.Content
        };
    }

    public async Task<GeneratedScriptDto> GenerateScriptAsync(string prompt, string genre, string tone)
    {
        // In a real application, you would use a service like GPT-3 or another AI model to generate the script.
        // For this example, we'll just return a dummy script.
        var generatedContent = $"This is a generated script based on the prompt: '{prompt}', with a {genre} genre and a {tone} tone.";

        return new GeneratedScriptDto
        {
            Title = "Generated Script",
            Content = generatedContent
        };
    }
}