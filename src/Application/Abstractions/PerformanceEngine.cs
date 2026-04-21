using InvestmentPerformanceAttribution.Domain.Entities;

namespace InvestmentPerformanceAttribution.Application.Abstractions;

public interface IPerformanceEngine
{
    Task<PerformanceResult> CalculateAsync(CalculationJob job, CancellationToken cancellationToken);
}
