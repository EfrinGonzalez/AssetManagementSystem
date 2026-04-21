# Key Engineering Decisions

- Chosen Clean Architecture for explicit boundaries and testability.
- Chosen in-memory seed strategy for deterministic offline execution.
- Chosen BackgroundService for async job orchestration and easy cloud migration.
- Added observability hooks early (correlation, request logs, OTEL extension points).
