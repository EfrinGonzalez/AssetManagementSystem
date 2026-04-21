namespace InvestmentPerformanceAttribution.Domain.Entities;

public sealed record PerformanceCalculationRequest(
    Guid PortfolioId,
    DateOnly StartDate,
    DateOnly EndDate,
    Guid? CorrelationId);
