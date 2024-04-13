using Microsoft.EntityFrameworkCore;
using MySchool.Core.Application.Interfaces.Repositories;
using MySchool.Core.Application.Interfaces.Services;
using MySchool.Core.Application.Services;
using MySchool.Infrastructure.Persistence.Context;
using MySchool.Infrastructure.Persistence.Repositories;

namespace MySchool.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRoleRepository, RoleRepository>();
                
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IRoleService, RoleService>();

        }

        public static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
        {
            return services
                .AddDbContext<StudContext>(a => a.UseMySQL(connectionString));

        }
    }
}
