using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace InvestmentPerformanceAttribution.IntegrationTests.Api;

public sealed class PortfolioEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PortfolioEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPortfolios_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/v1/portfolios");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
