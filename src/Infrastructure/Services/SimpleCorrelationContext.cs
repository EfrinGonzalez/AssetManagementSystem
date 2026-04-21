using InvestmentPerformanceAttribution.Application.Abstractions;

namespace InvestmentPerformanceAttribution.Infrastructure.Services;

public sealed class SimpleCorrelationContext : ICorrelationContext
{
    public Guid CorrelationId => Guid.NewGuid();
}
