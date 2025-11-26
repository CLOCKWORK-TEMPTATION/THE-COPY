using System.Threading.Tasks;
using TheCopy.Application.Interfaces.AI;

namespace TheCopy.Infrastructure.Services.AI;

public class MockGenerativeAiService : IGenerativeAiService
{
    public Task<string> GenerateContent(string prompt)
    {
        return Task.FromResult($"Mock response for: {prompt}");
    }
}
