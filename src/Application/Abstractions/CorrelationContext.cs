namespace InvestmentPerformanceAttribution.Application.Abstractions;

public interface ICorrelationContext
{
    Guid CorrelationId { get; }
}
