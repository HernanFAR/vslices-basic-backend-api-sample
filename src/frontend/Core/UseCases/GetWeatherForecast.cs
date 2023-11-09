using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VSlices.Core.Abstracts.Handlers;
using VSlices.Core.Abstracts.Presentation;
using VSlices.Core.Abstracts.Requests;
using VSlices.Core.Abstracts.Responses;

// ReSharper disable once CheckNamespace
namespace Core.UseCases.GetWeatherForecast;

public class GetWeatherForecastDependencies : IFeatureDependencyDefinition
{
    public static void DefineDependencies(IServiceCollection services)
    {

    }
}

public record GetWeatherForecastQuery(DateOnly StartDate) : IQuery<GetWeatherForecastDto[]>;

public record GetWeatherForecastDto(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class GetWeatherForecastHandler : IHandler<GetWeatherForecastQuery, GetWeatherForecastDto[]>
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public async ValueTask<Response<GetWeatherForecastDto[]>> HandleAsync(GetWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        return Enumerable.Range(1, 5)
            .Select(index =>
                new GetWeatherForecastDto(
                    request.StartDate.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)])
            )
            .ToArray();
    }
}

public class GetWeatherForecastValidator : AbstractValidator<GetWeatherForecastQuery>
{
    public GetWeatherForecastValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now));
    }
}
