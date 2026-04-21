namespace InvestmentPerformanceAttribution.Api.Middleware;

public sealed class CorrelationIdMiddleware(RequestDelegate next)
{
    private const string Header = "X-Correlation-Id";

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(Header, out var correlationId) || string.IsNullOrWhiteSpace(correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
        }

        context.Response.Headers[Header] = correlationId.ToString();
        context.Items[Header] = correlationId.ToString();

        await next(context);
    }
}
