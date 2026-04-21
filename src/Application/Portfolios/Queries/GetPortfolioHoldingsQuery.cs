using InvestmentPerformanceAttribution.Application.Abstractions;
using InvestmentPerformanceAttribution.Application.Common.Models;
using MediatR;

namespace InvestmentPerformanceAttribution.Application.Portfolios.Queries;

public sealed record GetPortfolioHoldingsQuery(Guid PortfolioId) : IRequest<IReadOnlyCollection<PositionDto>>;

public sealed class GetPortfolioHoldingsQueryHandler(IPortfolioRepository portfolioRepository, IInstrumentRepository instrumentRepository)
    : IRequestHandler<GetPortfolioHoldingsQuery, IReadOnlyCollection<PositionDto>>
{
    public async Task<IReadOnlyCollection<PositionDto>> Handle(GetPortfolioHoldingsQuery request, CancellationToken cancellationToken)
    {
        var portfolio = await portfolioRepository.GetByIdAsync(request.PortfolioId, cancellationToken);
        if (portfolio is null)
        {
            return Array.Empty<PositionDto>();
        }

        var instruments = await instrumentRepository.GetByIdsAsync(portfolio.Positions.Select(x => x.InstrumentId), cancellationToken);

        return portfolio.Positions
            .Select(position =>
            {
                var instrument = instruments[position.InstrumentId];
                return new PositionDto(
                    position.InstrumentId,
                    position.Quantity,
                    position.Weight,
                    instrument.Ticker,
                    instrument.Name,
                    instrument.AssetClass,
                    instrument.Sector,
                    instrument.Country);
            })
            .ToArray();
    }
}
