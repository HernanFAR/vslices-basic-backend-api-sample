using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting;

public static class CrossCuttingDependencies
{
    public static IServiceCollection AddCrossCuttingDependencies(this IServiceCollection services)
    {
        return services
            .AddLoggingBehavior()
            .AddFluentValidationBehavior();
    }
}
