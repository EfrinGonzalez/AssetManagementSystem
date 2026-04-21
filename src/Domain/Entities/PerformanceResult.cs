namespace InvestmentPerformanceAttribution.Domain.Entities;

public sealed record PerformanceResult(
    Guid JobId,
    Guid PortfolioId,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal PortfolioReturn,
    decimal BenchmarkReturn,
    decimal ActiveReturn,
    AttributionResult Attribution,
    DateTimeOffset CalculatedAtUtc);
