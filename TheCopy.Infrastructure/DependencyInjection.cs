using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheCopy.Application.Interfaces;
using TheCopy.Application.Interfaces.AI;
using TheCopy.Infrastructure.Data;
using TheCopy.Infrastructure.Repositories;
using TheCopy.Infrastructure.Services.AI;

namespace TheCopy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure EF Core with PostgreSQL
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));

        // Register Repositories
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IScriptRepository, ScriptRepository>();

        // Register AI Service
        services.AddScoped<IGenerativeAiService, GenerativeAiService>();

        return services;
    }
}
