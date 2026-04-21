using FluentValidation;
using InvestmentPerformanceAttribution.Application.Abstractions;
using MediatR;

namespace InvestmentPerformanceAttribution.Application.Performance.Commands;

public sealed record SubmitPerformanceCalculationCommand(Guid PortfolioId, DateOnly StartDate, DateOnly EndDate) : IRequest<Guid>;

public sealed class SubmitPerformanceCalculationCommandValidator : AbstractValidator<SubmitPerformanceCalculationCommand>
{
    public SubmitPerformanceCalculationCommandValidator()
    {
        RuleFor(x => x.PortfolioId).NotEmpty();
        RuleFor(x => x.StartDate).LessThanOrEqualTo(x => x.EndDate);
    }
}

public sealed class SubmitPerformanceCalculationCommandHandler(
    IPortfolioRepository portfolioRepository,
    ICalculationJobRepository jobRepository,
    ICorrelationContext correlationContext)
    : IRequestHandler<SubmitPerformanceCalculationCommand, Guid>
{
    public async Task<Guid> Handle(SubmitPerformanceCalculationCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await portfolioRepository.GetByIdAsync(request.PortfolioId, cancellationToken);
        if (portfolio is null)
        {
            throw new InvalidOperationException($"Portfolio '{request.PortfolioId}' not found.");
        }

        var job = await jobRepository.CreateAsync(
            new Domain.Entities.PerformanceCalculationRequest(request.PortfolioId, request.StartDate, request.EndDate, correlationContext.CorrelationId),
            cancellationToken);

        return job.JobId;
    }
}
