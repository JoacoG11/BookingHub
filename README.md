# BookingHub Backend API

BookingHub is a backend REST API designed to manage reservations for gym classes, rooms or similar resources.
The system allows users to register, authenticate, check availability and create bookings, while administrators
can manage resources and schedules.

This project is intended as a professional portfolio project, following clean architecture principles
and common backend best practices.

---

## Tech Stack (planned)

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- Relational Database (SQL Server or PostgreSQL)
- JWT Authentication
- Docker (later stage)
- xUnit for testing

---

## Use Cases

### User
- Register and log in.
- View available resources and schedules.
- Create and cancel bookings.
- View personal booking history.

### Admin
- Create, update and deactivate resources.
- Define available time slots for each resource.
- View bookings by resource and date range.

Detailed use cases can be found in:  
`docs/use-cases.md` 

---

## Architecture

This project follows **Clean Architecture** principles, with a clear separation of concerns
between domain logic, application use cases, infrastructure and delivery mechanisms.

The main goals of this architecture are:
- Independence from frameworks
- High testability
- Clear business rules
- Easy maintenance and scalability

### Layers

- **Domain**
  - Core business entities
  - Business rules and invariants
  - Repository interfaces

- **Application**
  - Use cases and business workflows
  - Application services
  - DTOs and mapping logic
  - Depends only on Domain

- **Infrastructure**
  - Database access (Entity Framework Core)
  - Repository implementations
  - Persistence configuration

- **API**
  - HTTP controllers
  - Authentication & authorization
  - Middleware
  - Dependency injection configuration

---

## Getting Started

### Prerequisites

- .NET SDK 10
- Docker (opcional, para ejecución en contenedores)
- Git

---

## Running the Project

### Run locally with .NET

Desde la raíz del proyecto:

```bash
dotnet build
dotnet run --project src/Booking.Api
```
### Open on:

http://localhost:7778/swagger

### Run with docker

```bash
docker compose up --build -d
```

## Open on:

http://localhost:7778/swagger


```bash
docker compose down
```

---

## Run All Tests

```bash
dotnet test
```

### Run Tests by Layer

```bash
dotnet test src/tests/Booking.Domain.Tests
```

```bash
dotnet test src/tests/Booking.Application.Tests
```

```bash
dotnet test src/tests/Booking.Api.IntegrationTests
```

