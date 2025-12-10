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
`docs/03-use-cases.md` 

---

## Repository Structure

```text
src/    -> Application source code  
tests/  -> Automated tests  
docs/   -> Project documentation  
