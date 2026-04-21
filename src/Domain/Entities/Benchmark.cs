namespace InvestmentPerformanceAttribution.Domain.Entities;

public sealed record Benchmark(
    Guid Id,
    string Name,
    string Currency);
