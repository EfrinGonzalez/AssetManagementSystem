using InvestmentPerformanceAttribution.Application.Abstractions;
using InvestmentPerformanceAttribution.Application.Common.Models;
using MediatR;

namespace InvestmentPerformanceAttribution.Application.Portfolios.Queries;

public sealed record GetPortfolioByIdQuery(Guid PortfolioId) : IRequest<PortfolioDto?>;

public sealed class GetPortfolioByIdQueryHandler(IPortfolioRepository repository) : IRequestHandler<GetPortfolioByIdQuery, PortfolioDto?>
{
    public async Task<PortfolioDto?> Handle(GetPortfolioByIdQuery request, CancellationToken cancellationToken)
    {
        var portfolio = await repository.GetByIdAsync(request.PortfolioId, cancellationToken);
        return portfolio is null ? null : new PortfolioDto(portfolio.Id, portfolio.Name, portfolio.BaseCurrency, portfolio.BenchmarkId);
    }
}
