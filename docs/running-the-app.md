# Running the App

## dotnet
- `dotnet run --project src/Api/Api.csproj`

## docker
- `docker build -t investment-api .`
- `docker run -p 8080:8080 investment-api`

Use `src/Api/InvestmentPerformanceAttribution.http` for sample requests.
