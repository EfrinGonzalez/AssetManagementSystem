using InvestmentPerformanceAttribution.Application.Abstractions;
using InvestmentPerformanceAttribution.Infrastructure.Data;
using InvestmentPerformanceAttribution.Infrastructure.Jobs;
using InvestmentPerformanceAttribution.Infrastructure.Repositories;
using InvestmentPerformanceAttribution.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentPerformanceAttribution.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<InMemoryDataContext>();
        services.AddSingleton<IPortfolioRepository, InMemoryPortfolioRepository>();
        services.AddSingleton<IInstrumentRepository, InMemoryInstrumentRepository>();
        services.AddSingleton<IMarketDataRepository, InMemoryMarketDataRepository>();
        services.AddSingleton<ICalculationJobRepository, InMemoryCalculationJobRepository>();
        services.AddSingleton<ICorrelationContext, SimpleCorrelationContext>();
        services.AddSingleton<IPerformanceEngine, PerformanceEngine>();
        services.AddHostedService<CalculationWorker>();

        return services;
    }
}
