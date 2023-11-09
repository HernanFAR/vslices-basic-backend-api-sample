using Core;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class CoreDependencies
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        return services
            .AddReflectionSender()
            .AddHandlersFromAssemblyContaining<Anchor>()
            .AddFeatureDependenciesFromAssemblyContaining<Anchor>();
    }
}
