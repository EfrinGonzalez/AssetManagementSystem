namespace InvestmentPerformanceAttribution.Domain.Entities;

public sealed record PriceSnapshot(
    Guid InstrumentId,
    DateOnly Date,
    decimal Price);
