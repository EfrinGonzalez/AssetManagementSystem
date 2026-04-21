namespace InvestmentPerformanceAttribution.Domain.Entities;

public sealed record PortfolioSnapshot(
    Guid PortfolioId,
    DateOnly Date,
    decimal MarketValue,
    decimal NetCashFlow);
