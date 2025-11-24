using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheCopy.Application.Interfaces;
using TheCopy.Application.Interfaces.AI;
using TheCopy.Infrastructure.Data;
using TheCopy.Infrastructure.Repositories;
using TheCopy.Infrastructure.Services.AI;

namespace TheCopy.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TheCopyDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IScriptRepository, ScriptRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddSingleton<IGenerativeAiService, MockGenerativeAiService>();

        return services;
    }
}
