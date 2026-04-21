using InvestmentPerformanceAttribution.Domain.Entities;
using InvestmentPerformanceAttribution.Domain.Enums;

namespace InvestmentPerformanceAttribution.Application.Common.Models;

public sealed record PortfolioDto(Guid Id, string Name, string BaseCurrency, Guid BenchmarkId);

public sealed record PositionDto(Guid InstrumentId, decimal Quantity, decimal Weight, string Ticker, string Name, AssetClass AssetClass, string Sector, string Country);

public sealed record InstrumentDto(Guid Id, string Ticker, string Name, AssetClass AssetClass, string Sector, string Country, string Currency);

public sealed record CalculationStatusDto(Guid JobId, string Status, string? Error, DateTimeOffset CreatedAtUtc, DateTimeOffset? UpdatedAtUtc);

public sealed record PerformanceResultDto(
    Guid JobId,
    Guid PortfolioId,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal PortfolioReturn,
    decimal BenchmarkReturn,
    decimal ActiveReturn,
    AttributionResult Attribution,
    DateTimeOffset CalculatedAtUtc);
