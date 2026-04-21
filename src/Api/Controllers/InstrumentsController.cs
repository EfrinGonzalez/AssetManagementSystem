using InvestmentPerformanceAttribution.Application.Portfolios.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPerformanceAttribution.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/instruments")]
public sealed class InstrumentsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetInstruments(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetInstrumentsQuery(), cancellationToken);
        return Ok(result);
    }
}
