using Microsoft.Extensions.Logging;

namespace CrossCutting.Connections;

public class QuestionApiConfiguration
{
    public string Url { get; init; } = string.Empty;
}

public partial class QuestionApiConnection
{
    private readonly ILogger<QuestionApiConnection> _logger;

    public QuestionApiConnection(ILogger<QuestionApiConnection> logger,
        QuestionApiConfiguration config, HttpClient client) : this(config.Url, client)
    { 
        _logger = logger;
    }

    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
    {
        _logger.LogInformation("Request: {Method} at {Uri}: {Body}",
            request.Method, url, request.Content?.ReadAsStringAsync().Result);

    }
}
