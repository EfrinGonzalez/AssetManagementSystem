using InvestmentPerformanceAttribution.Application.Abstractions;
using InvestmentPerformanceAttribution.Application.Common.Models;
using MediatR;

namespace InvestmentPerformanceAttribution.Application.Portfolios.Queries;

public sealed record GetInstrumentsQuery : IRequest<IReadOnlyCollection<InstrumentDto>>;

public sealed class GetInstrumentsQueryHandler(IInstrumentRepository repository) : IRequestHandler<GetInstrumentsQuery, IReadOnlyCollection<InstrumentDto>>
{
    public async Task<IReadOnlyCollection<InstrumentDto>> Handle(GetInstrumentsQuery request, CancellationToken cancellationToken)
    {
        var instruments = await repository.GetAllAsync(cancellationToken);
        return instruments.Select(i => new InstrumentDto(i.Id, i.Ticker, i.Name, i.AssetClass, i.Sector, i.Country, i.Currency)).ToArray();
    }
}
