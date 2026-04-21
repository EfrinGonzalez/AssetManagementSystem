# ADR-0002: Async Calculation Jobs via BackgroundService

Status: Accepted

Context: Performance calculations can be long-running and should not block API clients.
Decision: Persist queued jobs and process via hosted background worker.
Consequences: Polling UX, easy migration to queue-based distributed workers later.
