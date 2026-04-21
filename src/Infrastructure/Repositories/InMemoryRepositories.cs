using System.Collections.Concurrent;
using InvestmentPerformanceAttribution.Application.Abstractions;
using InvestmentPerformanceAttribution.Domain.Entities;
using InvestmentPerformanceAttribution.Infrastructure.Data;

namespace InvestmentPerformanceAttribution.Infrastructure.Repositories;

public sealed class InMemoryPortfolioRepository(InMemoryDataContext context) : IPortfolioRepository
{
    public Task<IReadOnlyCollection<Portfolio>> GetAllAsync(CancellationToken cancellationToken) => Task.FromResult(context.Portfolios);

    public Task<Portfolio?> GetByIdAsync(Guid portfolioId, CancellationToken cancellationToken)
        => Task.FromResult(context.Portfolios.SingleOrDefault(x => x.Id == portfolioId));
}

public sealed class InMemoryInstrumentRepository(InMemoryDataContext context) : IInstrumentRepository
{
    public Task<IReadOnlyCollection<Instrument>> GetAllAsync(CancellationToken cancellationToken) => Task.FromResult(context.Instruments);

    public Task<IReadOnlyDictionary<Guid, Instrument>> GetByIdsAsync(IEnumerable<Guid> instrumentIds, CancellationToken cancellationToken)
    {
        var hash = instrumentIds.ToHashSet();
        var map = context.Instruments.Where(i => hash.Contains(i.Id)).ToDictionary(k => k.Id, v => v);
        return Task.FromResult((IReadOnlyDictionary<Guid, Instrument>)map);
    }
}

public sealed class InMemoryMarketDataRepository(InMemoryDataContext context) : IMarketDataRepository
{
    public Task<IReadOnlyCollection<PriceSnapshot>> GetPriceHistoryAsync(IEnumerable<Guid> instrumentIds, DateOnly start, DateOnly end, CancellationToken cancellationToken)
    {
        var ids = instrumentIds.ToHashSet();
        var data = context.Prices.Where(x => ids.Contains(x.InstrumentId) && x.Date >= start && x.Date <= end).ToArray();
        return Task.FromResult((IReadOnlyCollection<PriceSnapshot>)data);
    }

    public Task<IReadOnlyCollection<BenchmarkSnapshot>> GetBenchmarkHistoryAsync(Guid benchmarkId, DateOnly start, DateOnly end, CancellationToken cancellationToken)
    {
        var data = context.BenchmarkSnapshots.Where(x => x.BenchmarkId == benchmarkId && x.Date >= start && x.Date <= end).ToArray();
        return Task.FromResult((IReadOnlyCollection<BenchmarkSnapshot>)data);
    }

    public Task<IReadOnlyCollection<PortfolioSnapshot>> GetPortfolioSnapshotsAsync(Guid portfolioId, DateOnly start, DateOnly end, CancellationToken cancellationToken)
    {
        var data = context.PortfolioSnapshots.Where(x => x.PortfolioId == portfolioId && x.Date >= start && x.Date <= end).OrderBy(x => x.Date).ToArray();
        return Task.FromResult((IReadOnlyCollection<PortfolioSnapshot>)data);
    }
}

public sealed class InMemoryCalculationJobRepository : ICalculationJobRepository
{
    private readonly ConcurrentDictionary<Guid, CalculationJob> _jobs = new();

    public Task<CalculationJob> CreateAsync(PerformanceCalculationRequest request, CancellationToken cancellationToken)
    {
        var job = new CalculationJob
        {
            JobId = Guid.NewGuid(),
            PortfolioId = request.PortfolioId,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        _jobs[job.JobId] = job;
        return Task.FromResult(job);
    }

    public Task<CalculationJob?> GetAsync(Guid jobId, CancellationToken cancellationToken)
        => Task.FromResult(_jobs.GetValueOrDefault(jobId));

    public Task<IReadOnlyCollection<CalculationJob>> GetQueuedAsync(int batchSize, CancellationToken cancellationToken)
    {
        var queued = _jobs.Values.Where(x => x.Status == CalculationJobStatus.Queued).Take(batchSize).ToArray();
        return Task.FromResult((IReadOnlyCollection<CalculationJob>)queued);
    }

    public Task UpdateAsync(CalculationJob job, CancellationToken cancellationToken)
    {
        _jobs[job.JobId] = job;
        return Task.CompletedTask;
    }
}
