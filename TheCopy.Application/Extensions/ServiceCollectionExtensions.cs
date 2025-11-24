using Microsoft.Extensions.DependencyInjection;
using TheCopy.Application.Interfaces;
using TheCopy.Application.Services;

namespace TheCopy.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IScriptService, ScriptService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
