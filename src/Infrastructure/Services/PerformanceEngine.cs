using InvestmentPerformanceAttribution.Application.Abstractions;
using InvestmentPerformanceAttribution.Domain.Calculations;
using InvestmentPerformanceAttribution.Domain.Entities;

namespace InvestmentPerformanceAttribution.Infrastructure.Services;

public sealed class PerformanceEngine(
    IPortfolioRepository portfolioRepository,
    IInstrumentRepository instrumentRepository,
    IMarketDataRepository marketDataRepository) : IPerformanceEngine
{
    public async Task<PerformanceResult> CalculateAsync(CalculationJob job, CancellationToken cancellationToken)
    {
        var portfolio = await portfolioRepository.GetByIdAsync(job.PortfolioId, cancellationToken)
            ?? throw new InvalidOperationException("Portfolio not found.");

        var snapshots = (await marketDataRepository.GetPortfolioSnapshotsAsync(job.PortfolioId, job.StartDate, job.EndDate, cancellationToken)).OrderBy(x => x.Date).ToArray();
        if (snapshots.Length < 2)
        {
            throw new InvalidOperationException("Insufficient portfolio snapshots for performance calculation.");
        }

        var periods = new List<(decimal StartValue, decimal EndValue, decimal CashFlow)>();
        for (var i = 1; i < snapshots.Length; i++)
        {
            periods.Add((snapshots[i - 1].MarketValue, snapshots[i].MarketValue, snapshots[i].NetCashFlow));
        }

        var portfolioReturn = TwrCalculator.Calculate(periods);

        var benchmarkSeries = (await marketDataRepository.GetBenchmarkHistoryAsync(portfolio.BenchmarkId, job.StartDate, job.EndDate, cancellationToken)).OrderBy(x => x.Date).ToArray();
        if (benchmarkSeries.Length < 2)
        {
            throw new InvalidOperationException("Insufficient benchmark data.");
        }

        var benchmarkReturn = (benchmarkSeries[^1].Level - benchmarkSeries[0].Level) / benchmarkSeries[0].Level;
        var activeReturn = portfolioReturn - benchmarkReturn;

        var instruments = await instrumentRepository.GetByIdsAsync(portfolio.Positions.Select(p => p.InstrumentId), cancellationToken);
        var byAssetClass = portfolio.Positions
            .GroupBy(p => instruments[p.InstrumentId].AssetClass.ToString())
            .Select(g => BuildBucket(g.Key, g.Sum(x => x.Weight), activeReturn))
            .ToArray();
        var bySector = portfolio.Positions
            .GroupBy(p => instruments[p.InstrumentId].Sector)
            .Select(g => BuildBucket(g.Key, g.Sum(x => x.Weight), activeReturn))
            .ToArray();
        var byCountry = portfolio.Positions
            .GroupBy(p => instruments[p.InstrumentId].Country)
            .Select(g => BuildBucket(g.Key, g.Sum(x => x.Weight), activeReturn))
            .ToArray();

        return new PerformanceResult(
            job.JobId,
            job.PortfolioId,
            job.StartDate,
            job.EndDate,
            decimal.Round(portfolioReturn, 6),
            decimal.Round(benchmarkReturn, 6),
            decimal.Round(activeReturn, 6),
            new AttributionResult(byAssetClass, bySector, byCountry),
            DateTimeOffset.UtcNow);
    }

    private static AttributionBucket BuildBucket(string key, decimal weight, decimal activeReturn)
    {
        var allocationEffect = decimal.Round(activeReturn * weight * 0.6m, 6);
        var selectionEffect = decimal.Round(activeReturn * weight * 0.4m, 6);
        return new AttributionBucket(key, allocationEffect, selectionEffect, decimal.Round(allocationEffect + selectionEffect, 6));
    }
}
