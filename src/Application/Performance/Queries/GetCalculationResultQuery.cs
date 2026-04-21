using InvestmentPerformanceAttribution.Application.Abstractions;
using InvestmentPerformanceAttribution.Application.Common.Models;
using MediatR;

namespace InvestmentPerformanceAttribution.Application.Performance.Queries;

public sealed record GetCalculationResultQuery(Guid JobId) : IRequest<PerformanceResultDto?>;

public sealed class GetCalculationResultQueryHandler(ICalculationJobRepository repository)
    : IRequestHandler<GetCalculationResultQuery, PerformanceResultDto?>
{
    public async Task<PerformanceResultDto?> Handle(GetCalculationResultQuery request, CancellationToken cancellationToken)
    {
        var job = await repository.GetAsync(request.JobId, cancellationToken);
        if (job?.Result is null)
        {
            return null;
        }

        var result = job.Result;
        return new PerformanceResultDto(
            result.JobId,
            result.PortfolioId,
            result.StartDate,
            result.EndDate,
            result.PortfolioReturn,
            result.BenchmarkReturn,
            result.ActiveReturn,
            result.Attribution,
            result.CalculatedAtUtc);
    }
}
