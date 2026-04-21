using FluentAssertions;
using InvestmentPerformanceAttribution.Domain.Calculations;

namespace InvestmentPerformanceAttribution.UnitTests.Domain;

public sealed class TwrCalculatorTests
{
    [Fact]
    public void Calculate_ShouldReturnExpectedTwr()
    {
        var periods = new[]
        {
            (100m, 105m, 0m),
            (105m, 108m, 1m)
        };

        var result = TwrCalculator.Calculate(periods);

        result.Should().BeApproximately(0.07m, 0.01m);
    }
}
