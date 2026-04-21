# Investment Performance & Attribution API Platform

Production-oriented .NET 8 backend for portfolio performance, benchmark comparison, and attribution with asynchronous job processing.

## Implementation Summary
- Clean architecture split into `src/Api`, `src/Application`, `src/Domain`, `src/Infrastructure`.
- CQRS with MediatR, FluentValidation, and pipeline behaviors (validation/logging/performance).
- Async calculation lifecycle with job queue + background worker.
- In-memory seeded market/portfolio data for deterministic offline runs.
- Swagger/OpenAPI, health checks, ProblemDetails, correlation IDs, request logging, OpenTelemetry hooks.
- Unit and integration test projects with FluentAssertions.
- Containerization, Azure Container Apps Bicep IaC, Azure DevOps pipeline.

## Quickstart
```bash
dotnet restore InvestmentPerformanceAttribution.sln
dotnet run --project src/Api/Api.csproj
```

API base: `http://localhost:5000/api/v1`

## Local to Cloud
1. Local: `dotnet run --project src/Api/Api.csproj`
2. Docker: `docker build -t investment-api . && docker run -p 8080:8080 investment-api`
3. Bicep deploy: `az deployment group create --resource-group <rg> --template-file infra/main.bicep --parameters @infra/main.parameters.json`
4. Azure DevOps: configure `azure-pipelines.yml` and service connections.

## Sample curl
```bash
curl http://localhost:5000/api/v1/portfolios
curl -X POST http://localhost:5000/api/v1/performance-calculations -H "Content-Type: application/json" -d '{"portfolioId":"aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa","startDate":"2025-01-01","endDate":"2025-01-10"}'
```

See `/docs` for full architecture and deployment guidance.
