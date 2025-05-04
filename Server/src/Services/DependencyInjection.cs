using DatabaseLayout.Context;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Repository;

namespace Services;

public static class DependencyInjection
{
    /// <summary>
    /// Add all dependencies from Server project.
    /// </summary>
    /// <param name="services">Services.</param>
    /// <returns></returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IPortService, PortService>();
        services.AddScoped<IShipService, ShipService>();
        services.AddScoped<IVoyageService, VoyageService>();


        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPortTrackerContext, PortTrackerContext>();
        services.AddScoped<IPortRepository, PortRepository>();
        services.AddScoped<IVoyageRepository, VoyageRepository>();

        return services;
    }
}