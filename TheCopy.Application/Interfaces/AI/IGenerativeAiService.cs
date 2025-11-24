using System.Threading.Tasks;

namespace TheCopy.Application.Interfaces.AI
{
    public interface IGenerativeAiService
    {        
        Task<string> GenerateContent(string prompt);
    }
}
