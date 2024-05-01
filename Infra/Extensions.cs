using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using Infra.Ports;
using Infra.StorageAdapter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infra;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));
        services.AddScoped<IStorage, LocalFileStorage>();
        return services;
    }
}