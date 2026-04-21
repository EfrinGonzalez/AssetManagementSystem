namespace InvestmentPerformanceAttribution.Domain.Calculations;

public static class TwrCalculator
{
    public static decimal Calculate(IReadOnlyCollection<(decimal StartValue, decimal EndValue, decimal CashFlow)> periods)
    {
        if (periods.Count == 0)
        {
            return 0m;
        }

        var cumulative = 1m;
        foreach (var (startValue, endValue, cashFlow) in periods)
        {
            if (startValue == 0m)
            {
                continue;
            }

            var subPeriodReturn = (endValue - cashFlow - startValue) / startValue;
            cumulative *= (1m + subPeriodReturn);
        }

        return cumulative - 1m;
    }
}
