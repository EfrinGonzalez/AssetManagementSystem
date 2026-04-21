namespace InvestmentPerformanceAttribution.Domain.Entities;

public sealed class Portfolio
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string BaseCurrency { get; init; } = "USD";
    public Guid BenchmarkId { get; init; }
    public IReadOnlyCollection<Position> Positions { get; init; } = Array.Empty<Position>();
}
