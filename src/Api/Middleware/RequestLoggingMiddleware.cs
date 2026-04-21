using Microsoft.Extensions.Primitives;

namespace InvestmentPerformanceAttribution.Api.Middleware;

public sealed class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        context.Request.Headers.TryGetValue("X-Correlation-Id", out StringValues correlationId);
        logger.LogInformation("HTTP {Method} {Path} CorrelationId={CorrelationId}", context.Request.Method, context.Request.Path, correlationId.ToString());
        await next(context);
    }
}
