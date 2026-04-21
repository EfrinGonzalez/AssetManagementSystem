# ADR-0001: Use Clean Architecture + CQRS

Status: Accepted

Context: Need clear separation between domain, application use-cases, API adapters, and infrastructure details.
Decision: Adopt clean layered architecture with MediatR handlers.
Consequences: Slightly more boilerplate, improved maintainability and testability.
