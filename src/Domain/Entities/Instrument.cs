using InvestmentPerformanceAttribution.Domain.Enums;

namespace InvestmentPerformanceAttribution.Domain.Entities;

public sealed record Instrument(
    Guid Id,
    string Ticker,
    string Name,
    AssetClass AssetClass,
    string Sector,
    string Country,
    string Currency);
