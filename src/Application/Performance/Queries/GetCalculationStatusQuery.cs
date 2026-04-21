using InvestmentPerformanceAttribution.Application.Abstractions;
using InvestmentPerformanceAttribution.Application.Common.Models;
using MediatR;

namespace InvestmentPerformanceAttribution.Application.Performance.Queries;

public sealed record GetCalculationStatusQuery(Guid JobId) : IRequest<CalculationStatusDto?>;

public sealed class GetCalculationStatusQueryHandler(ICalculationJobRepository repository)
    : IRequestHandler<GetCalculationStatusQuery, CalculationStatusDto?>
{
    public async Task<CalculationStatusDto?> Handle(GetCalculationStatusQuery request, CancellationToken cancellationToken)
    {
        var job = await repository.GetAsync(request.JobId, cancellationToken);
        return job is null
            ? null
            : new CalculationStatusDto(job.JobId, job.Status.ToString(), job.Error, job.CreatedAtUtc, job.UpdatedAtUtc);
    }
}
