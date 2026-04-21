using InvestmentPerformanceAttribution.Domain.Entities;
using InvestmentPerformanceAttribution.Domain.Enums;

namespace InvestmentPerformanceAttribution.Infrastructure.Data;

public sealed class InMemoryDataContext
{
    public IReadOnlyCollection<Instrument> Instruments { get; }
    public IReadOnlyCollection<Portfolio> Portfolios { get; }
    public IReadOnlyCollection<Benchmark> Benchmarks { get; }
    public IReadOnlyCollection<PriceSnapshot> Prices { get; }
    public IReadOnlyCollection<PortfolioSnapshot> PortfolioSnapshots { get; }
    public IReadOnlyCollection<BenchmarkSnapshot> BenchmarkSnapshots { get; }

    public InMemoryDataContext()
    {
        var msft = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var aapl = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var agg = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var cash = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var sap = Guid.Parse("55555555-5555-5555-5555-555555555555");

        var benchmarkId = Guid.Parse("99999999-9999-9999-9999-999999999999");

        Instruments =
        [
            new Instrument(msft, "MSFT", "Microsoft Corp", AssetClass.Equity, "Technology", "US", "USD"),
            new Instrument(aapl, "AAPL", "Apple Inc", AssetClass.Equity, "Technology", "US", "USD"),
            new Instrument(agg, "AGG", "US Aggregate Bond ETF", AssetClass.FixedIncome, "Government", "US", "USD"),
            new Instrument(cash, "USD", "US Dollar Cash", AssetClass.Cash, "Cash", "US", "USD"),
            new Instrument(sap, "SAP", "SAP SE", AssetClass.Equity, "Technology", "DE", "EUR")
        ];

        Portfolios =
        [
            new Portfolio
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Global Balanced",
                BaseCurrency = "USD",
                BenchmarkId = benchmarkId,
                Positions =
                [
                    new Position(msft, 1000m, 0.30m),
                    new Position(aapl, 1100m, 0.25m),
                    new Position(agg, 4000m, 0.35m),
                    new Position(cash, 1m, 0.10m)
                ]
            },
            new Portfolio
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "Global Equity Focus",
                BaseCurrency = "USD",
                BenchmarkId = benchmarkId,
                Positions =
                [
                    new Position(msft, 1200m, 0.40m),
                    new Position(aapl, 900m, 0.25m),
                    new Position(sap, 1400m, 0.35m)
                ]
            }
        ];

        Benchmarks = [new Benchmark(benchmarkId, "Global 60/40", "USD")];

        var start = new DateOnly(2025, 01, 01);
        var end = new DateOnly(2025, 01, 10);

        Prices = GeneratePriceHistory(msft, 300m, start, end, 1.004m)
            .Concat(GeneratePriceHistory(aapl, 190m, start, end, 1.003m))
            .Concat(GeneratePriceHistory(agg, 99m, start, end, 1.001m))
            .Concat(GeneratePriceHistory(cash, 1m, start, end, 1.0000m))
            .Concat(GeneratePriceHistory(sap, 160m, start, end, 1.002m))
            .ToArray();

        BenchmarkSnapshots = GenerateBenchmarkHistory(benchmarkId, 1000m, start, end, 1.0025m).ToArray();

        PortfolioSnapshots =
        [
            new PortfolioSnapshot(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), start, 10_000_000m, 0m),
            new PortfolioSnapshot(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), start.AddDays(3), 10_200_000m, 120_000m),
            new PortfolioSnapshot(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), end, 10_650_000m, 0m),
            new PortfolioSnapshot(Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), start, 7_000_000m, 0m),
            new PortfolioSnapshot(Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), start.AddDays(3), 7_250_000m, 75_000m),
            new PortfolioSnapshot(Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), end, 7_600_000m, 0m)
        ];
    }

    private static IReadOnlyCollection<PriceSnapshot> GeneratePriceHistory(Guid instrumentId, decimal startPrice, DateOnly start, DateOnly end, decimal dailyGrowth)
    {
        var values = new List<PriceSnapshot>();
        var cursor = start;
        var current = startPrice;
        while (cursor <= end)
        {
            values.Add(new PriceSnapshot(instrumentId, cursor, current));
            current *= dailyGrowth;
            cursor = cursor.AddDays(1);
        }

        return values;
    }

    private static IReadOnlyCollection<BenchmarkSnapshot> GenerateBenchmarkHistory(Guid benchmarkId, decimal startLevel, DateOnly start, DateOnly end, decimal dailyGrowth)
    {
        var values = new List<BenchmarkSnapshot>();
        var cursor = start;
        var current = startLevel;
        while (cursor <= end)
        {
            values.Add(new BenchmarkSnapshot(benchmarkId, cursor, current));
            current *= dailyGrowth;
            cursor = cursor.AddDays(1);
        }

        return values;
    }
}
