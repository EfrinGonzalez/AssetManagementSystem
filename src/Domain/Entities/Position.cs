namespace InvestmentPerformanceAttribution.Domain.Entities;

public sealed record Position(
    Guid InstrumentId,
    decimal Quantity,
    decimal Weight);
