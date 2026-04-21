using FluentAssertions;
using InvestmentPerformanceAttribution.Application;
using InvestmentPerformanceAttribution.Application.Performance.Commands;
using InvestmentPerformanceAttribution.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentPerformanceAttribution.UnitTests.Application;

public sealed class SubmitPerformanceCalculationCommandHandlerTests
{
    [Fact]
    public async Task Submit_ShouldCreateJob()
    {
        var services = new ServiceCollection();
        services.AddApplication();
        services.AddInfrastructure();
        var provider = services.BuildServiceProvider();

        var mediator = provider.GetRequiredService<IMediator>();

        var jobId = await mediator.Send(new SubmitPerformanceCalculationCommand(
            Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            new DateOnly(2025, 1, 1),
            new DateOnly(2025, 1, 10)));

        jobId.Should().NotBeEmpty();
    }
}
