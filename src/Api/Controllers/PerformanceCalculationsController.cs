using InvestmentPerformanceAttribution.Application.Performance.Commands;
using InvestmentPerformanceAttribution.Application.Performance.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPerformanceAttribution.Api.Controllers;

public sealed record SubmitPerformanceCalculationRequest(Guid PortfolioId, DateOnly StartDate, DateOnly EndDate);

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/performance-calculations")]
public sealed class PerformanceCalculationsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Submit([FromBody] SubmitPerformanceCalculationRequest request, CancellationToken cancellationToken)
    {
        var jobId = await mediator.Send(new SubmitPerformanceCalculationCommand(request.PortfolioId, request.StartDate, request.EndDate), cancellationToken);
        return AcceptedAtAction(nameof(GetStatus), new { version = "1", jobId }, new { jobId });
    }

    [HttpGet("{jobId:guid}")]
    public async Task<IActionResult> GetStatus(Guid jobId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCalculationStatusQuery(jobId), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("{jobId:guid}/result")]
    public async Task<IActionResult> GetResult(Guid jobId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCalculationResultQuery(jobId), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}
