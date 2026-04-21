namespace InvestmentPerformanceAttribution.Domain.Entities;

public sealed record AttributionResult(
    IReadOnlyCollection<AttributionBucket> ByAssetClass,
    IReadOnlyCollection<AttributionBucket> BySector,
    IReadOnlyCollection<AttributionBucket> ByCountry);

public sealed record AttributionBucket(
    string Key,
    decimal AllocationEffect,
    decimal SelectionEffect,
    decimal TotalEffect);
