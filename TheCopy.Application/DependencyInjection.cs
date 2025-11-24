using Microsoft.Extensions.DependencyInjection;
using TheCopy.Application.Interfaces;
using TheCopy.Application.Services;

namespace TheCopy.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IScriptService, ScriptService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
