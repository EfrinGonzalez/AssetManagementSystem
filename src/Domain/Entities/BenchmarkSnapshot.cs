namespace InvestmentPerformanceAttribution.Domain.Entities;

public sealed record BenchmarkSnapshot(
    Guid BenchmarkId,
    DateOnly Date,
    decimal Level);
