using InvestmentPerformanceAttribution.Application.Abstractions;
using InvestmentPerformanceAttribution.Application.Common.Models;
using MediatR;

namespace InvestmentPerformanceAttribution.Application.Portfolios.Queries;

public sealed record GetPortfoliosQuery : IRequest<IReadOnlyCollection<PortfolioDto>>;

public sealed class GetPortfoliosQueryHandler(IPortfolioRepository repository) : IRequestHandler<GetPortfoliosQuery, IReadOnlyCollection<PortfolioDto>>
{
    public async Task<IReadOnlyCollection<PortfolioDto>> Handle(GetPortfoliosQuery request, CancellationToken cancellationToken)
    {
        var portfolios = await repository.GetAllAsync(cancellationToken);
        return portfolios.Select(p => new PortfolioDto(p.Id, p.Name, p.BaseCurrency, p.BenchmarkId)).ToArray();
    }
}
