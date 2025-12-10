# API Design

This document describes the REST API exposed by BookingHub.

## API Versioning

- Base URL: /api/v1

## Authentication

- Authentication is handled using JWT tokens.
- Protected endpoints require the Authorization header:
  Authorization: Bearer <token>

## Endpoints (Initial Draft)

### Auth
- POST /api/v1/auth/register
- POST /api/v1/auth/login

### Resources
- GET /api/v1/resources
- POST /api/v1/resources
- PUT /api/v1/resources/{id}
- DELETE /api/v1/resources/{id}

### Bookings
- POST /api/v1/bookings
- GET /api/v1/bookings/my
- DELETE /api/v1/bookings/{id}

This document will be updated as the API evolves.
