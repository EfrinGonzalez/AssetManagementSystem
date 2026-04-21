namespace InvestmentPerformanceAttribution.Domain.Entities;

public enum CalculationJobStatus
{
    Queued,
    Running,
    Completed,
    Failed
}

public sealed class CalculationJob
{
    public Guid JobId { get; init; }
    public Guid PortfolioId { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public CalculationJobStatus Status { get; private set; } = CalculationJobStatus.Queued;
    public string? Error { get; private set; }
    public PerformanceResult? Result { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAtUtc { get; private set; }

    public void MarkRunning()
    {
        Status = CalculationJobStatus.Running;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
    }

    public void MarkFailed(string error)
    {
        Status = CalculationJobStatus.Failed;
        Error = error;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
    }

    public void MarkCompleted(PerformanceResult result)
    {
        Status = CalculationJobStatus.Completed;
        Result = result;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
    }
}
