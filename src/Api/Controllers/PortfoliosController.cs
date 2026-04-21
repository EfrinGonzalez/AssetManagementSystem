using InvestmentPerformanceAttribution.Application.Portfolios.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPerformanceAttribution.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/portfolios")]
public sealed class PortfoliosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPortfolios(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetPortfoliosQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetPortfolioByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("{id:guid}/holdings")]
    public async Task<IActionResult> GetHoldings(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetPortfolioHoldingsQuery(id), cancellationToken);
        return result.Count == 0 ? NotFound() : Ok(result);
    }
}
