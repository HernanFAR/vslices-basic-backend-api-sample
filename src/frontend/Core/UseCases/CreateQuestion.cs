using CrossCutting.Connections;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VSlices.Core.Abstracts.Handlers;
using VSlices.Core.Abstracts.Presentation;
using VSlices.Core.Abstracts.Requests;
using VSlices.Core.Abstracts.Responses;

// ReSharper disable once CheckNamespace
namespace Core.UseCases.CreateQuestion;

public class CreateQuestionDependencies : IFeatureDependencyDefinition
{
    public static void DefineDependencies(IServiceCollection services)
    {

    }
}

public class CreateQuestionRequest : IRequest
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

}

public class CreateQuestionHandler : IHandler<CreateQuestionRequest> 
{
    private readonly QuestionApiConnection _apiConnection;

    public CreateQuestionHandler(QuestionApiConnection apiConnection)
    {
        _apiConnection = apiConnection;
    }

    public async ValueTask<Response<Success>> HandleAsync(CreateQuestionRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _apiConnection.CreateQuestionAsync(new CreateQuestionContract
            {
                Content = request.Content,
                Title = request.Title
            }, cancellationToken);
        }
        catch (ApiException<HttpValidationProblemDetails> ex)
        {
            var errors = ex.Result.Errors
                .Select(pair => pair.Value.Select(e => new ValidationError(pair.Key, e)))
                .SelectMany(e => e)
                .ToArray();

            return BusinessFailure.Of.ContractValidation(errors: errors);
        }
        catch (ApiException)
        {
            return BusinessFailure.Of.Unspecified();
        }

        return Success.Value; 
    }
}

public class CreateQuestionValidator : AbstractValidator<CreateQuestionRequest>
{
    public const string TitleEmptyMessage = "El título no puede estar vacío";
    public const string TitleContainsPutaMessage = "El título no puede contener la palabra puta";
    public const string TitleMaxLengthMessage = "El título no puede tener más de {MaxLength} caracteres";

    public const string ContentEmptyMessage = "El contenido no puede estar vacío";
    public const string ContentMaxLengthMessage = "El contenido no puede tener más de {MaxLength} caracteres";

    public CreateQuestionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(TitleEmptyMessage)
            .Must(x => !x.Contains("puta")).WithMessage(TitleContainsPutaMessage)
            .MaximumLength(1024).WithMessage(TitleMaxLengthMessage);

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage(ContentEmptyMessage)
            .MaximumLength(128)
            .WithMessage(ContentMaxLengthMessage);
    }
}