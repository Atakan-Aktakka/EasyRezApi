// EasyRez.Infrastructure/DependencyInjection.cs

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Infrastructure.Persistence.Repositories;
using EasyRez.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyRez.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext
            // Npgsql'den SQL Server'a geçiş
           services.AddDbContext<EasyRezDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            return services;
        }
    }
}