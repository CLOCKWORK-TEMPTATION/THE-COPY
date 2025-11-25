using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheCopy.Application.Interfaces;
using TheCopy.Infrastructure.Data;
using TheCopy.Infrastructure.Services.AI;

namespace TheCopy.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        if (configuration.GetValue<bool>("UseMockAi"))
        {
            services.AddSingleton<IGenerativeAiService, MockGenerativeAiService>();
        }
        else
        {
            services.AddSingleton<IGenerativeAiService, GenerativeAiService>();
        }
    }
}
