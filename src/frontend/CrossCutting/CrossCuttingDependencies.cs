using CrossCutting.Connections;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class CrossCuttingDependencies
{
    public static IServiceCollection AddCrossCuttingDependencies(this IServiceCollection services)
    {
        return services
            .AddScoped<QuestionApiConnection>()
            .AddScoped(_ => 
                new QuestionApiConfiguration
                {
                    Url = "https://localhost:5001"
                })
            .AddHttpClient()
            .AddLoggingBehavior()
            .AddFluentValidationBehavior();
    }
}
