using System.Reflection;
using Domain.DrivingPort;
using Domain.Port.Driving;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(BaseMediatrHandler<,>)
                    .Assembly));

        return services;
    }
}