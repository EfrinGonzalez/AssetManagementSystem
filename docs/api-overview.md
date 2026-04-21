# API Overview

- `GET /api/v1/portfolios`
- `GET /api/v1/portfolios/{id}`
- `GET /api/v1/portfolios/{id}/holdings`
- `GET /api/v1/instruments`
- `POST /api/v1/performance-calculations`
- `GET /api/v1/performance-calculations/{jobId}`
- `GET /api/v1/performance-calculations/{jobId}/result`
- `GET /health`

```mermaid
sequenceDiagram
participant C as Client
participant A as API
participant J as Job Store
participant W as Worker
C->>A: POST calculation
A->>J: enqueue job
A-->>C: 202 + jobId
W->>J: poll queued
W->>W: run performance engine
W->>J: save result
C->>A: GET status/result
A->>J: read
A-->>C: status/result
```
