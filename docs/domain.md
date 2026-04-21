# Domain Model

Entities: Portfolio, Position, Instrument, Benchmark, PriceSnapshot, PortfolioSnapshot, PerformanceCalculationRequest, CalculationJob, PerformanceResult, AttributionResult.

Simplifications:
- TWR computed from snapshot sub-periods.
- Attribution effects are weighted active return decomposition.
- No corporate actions or intraday pricing.
- No FX translation engine; currency included for extension.

HPC evolution: move performance engine to distributed workers (AKS/Batch), queue-based fanout, and partitioned time-window calculations.
