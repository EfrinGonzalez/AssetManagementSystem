using InvestmentPerformanceAttribution.Application.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InvestmentPerformanceAttribution.Infrastructure.Jobs;

public sealed class CalculationWorker(
    ICalculationJobRepository jobRepository,
    IPerformanceEngine performanceEngine,
    ILogger<CalculationWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var jobs = await jobRepository.GetQueuedAsync(20, stoppingToken);

            foreach (var job in jobs)
            {
                try
                {
                    job.MarkRunning();
                    await jobRepository.UpdateAsync(job, stoppingToken);

                    var result = await performanceEngine.CalculateAsync(job, stoppingToken);
                    job.MarkCompleted(result);
                    await jobRepository.UpdateAsync(job, stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Calculation failed for job {JobId}", job.JobId);
                    job.MarkFailed(ex.Message);
                    await jobRepository.UpdateAsync(job, stoppingToken);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}
