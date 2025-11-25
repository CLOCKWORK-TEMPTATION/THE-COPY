using TheCopy.Shared.DataTransferObjects;
namespace TheCopy.Application.Interfaces;

public interface IScriptService
{
    Task<GeneratedScriptDto> CreateScriptAsync(CreateScriptRequestDto request);
    Task<GeneratedScriptDto> GenerateScriptAsync(string prompt, string genre, string tone);
}