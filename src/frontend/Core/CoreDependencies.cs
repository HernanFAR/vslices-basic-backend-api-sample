using Core.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class CoreDependencies
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        return services
            .AddScoped<WeatherForecastService>()
            .AddReflectionSender()
            .AddHandlersFromAssemblyContaining<Anchor>()
            .AddFeatureDependenciesFromAssemblyContaining<Anchor>();
    }
}
