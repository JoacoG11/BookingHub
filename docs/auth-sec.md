## Authentication & Security

### Authentication

The API uses **JWT (JSON Web Tokens)** for secure user authentication.

- Upon registration or login, users receive a signed JWT token.
- This token must be sent in the `Authorization` header as a Bearer token in every request to protected endpoints.

### Authorization

- Some endpoints (like resource creation) are restricted to users with the `Admin` role.
- Regular users can only view and manage their own bookings.
- Role-based access is handled via middleware and service-layer checks.

### Token Expiry

- JWTs are valid for a configurable time (e.g. 1 hour).
- Expired tokens will return `401 Unauthorized` responses.
- Token refresh functionality may be added in future versions.

### Password Handling

- User passwords are hashed using a secure algorithm (e.g. SHA256 or bcrypt) before storage.
- No plain-text passwords are stored at any point.

### Security Best Practices Implemented

- HTTPS enforced (especially in production).
- Input validation to avoid SQL injection and malformed payloads.
- Use of ASP.NET Core Identity best practices.
- Minimal external exposure in production Docker container.

---
