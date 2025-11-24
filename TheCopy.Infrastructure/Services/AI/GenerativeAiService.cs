
using Google.GenAI;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using TheCopy.Application.Interfaces.AI;

namespace TheCopy.Infrastructure.Services.AI;

public class GenerativeAiService : IGenerativeAiService
{
    private readonly string _apiKey;

    public GenerativeAiService(IConfiguration configuration)
    {
        _apiKey = configuration["AI:GeminiApiKey"];
    }

    public async Task<string> GenerateContent(string prompt)
    {
        var model = new GenerativeModel(apiKey: _apiKey);
        var response = await model.GenerateContentAsync(prompt);

        return response.Text;
    }
}
