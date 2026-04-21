using InvestmentPerformanceAttribution.Domain.Entities;

namespace InvestmentPerformanceAttribution.Application.Abstractions;

public interface IPortfolioRepository
{
    Task<IReadOnlyCollection<Portfolio>> GetAllAsync(CancellationToken cancellationToken);
    Task<Portfolio?> GetByIdAsync(Guid portfolioId, CancellationToken cancellationToken);
}

public interface IInstrumentRepository
{
    Task<IReadOnlyCollection<Instrument>> GetAllAsync(CancellationToken cancellationToken);
    Task<IReadOnlyDictionary<Guid, Instrument>> GetByIdsAsync(IEnumerable<Guid> instrumentIds, CancellationToken cancellationToken);
}

public interface IMarketDataRepository
{
    Task<IReadOnlyCollection<PriceSnapshot>> GetPriceHistoryAsync(IEnumerable<Guid> instrumentIds, DateOnly start, DateOnly end, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<BenchmarkSnapshot>> GetBenchmarkHistoryAsync(Guid benchmarkId, DateOnly start, DateOnly end, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<PortfolioSnapshot>> GetPortfolioSnapshotsAsync(Guid portfolioId, DateOnly start, DateOnly end, CancellationToken cancellationToken);
}

public interface ICalculationJobRepository
{
    Task<CalculationJob> CreateAsync(PerformanceCalculationRequest request, CancellationToken cancellationToken);
    Task<CalculationJob?> GetAsync(Guid jobId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<CalculationJob>> GetQueuedAsync(int batchSize, CancellationToken cancellationToken);
    Task UpdateAsync(CalculationJob job, CancellationToken cancellationToken);
}
