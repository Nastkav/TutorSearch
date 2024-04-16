using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using Infra.Ports;
using Infra.StorageAdapter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connectionString));
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStorage, LocalFileStorage>();
        return services;
    }
}