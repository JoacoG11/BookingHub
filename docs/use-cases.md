# Use Cases

## UC-01: User Registration

Actor: User

Description:
Allows a new user to register into the system using an email and password.

Preconditions:
- The email is not already registered.

Main Flow:
1. The user sends a registration request with email and password.
2. The system validates the input data.
3. The system stores the user with a hashed password.
4. The system returns a successful response.

Postconditions:
- A new user account is created.

---

## UC-02: User Login

Actor: User

Description:
Allows a registered user to log in and obtain an authentication token.

Preconditions:
- The user is registered.

Main Flow:
1. The user submits login credentials.
2. The system validates the credentials.
3. The system generates a JWT token.
4. The token is returned to the user.

---

## UC-03: Create Booking

Actor: User

Description:
Allows an authenticated user to create a booking for an available resource.

Preconditions:
- The user is authenticated.
- The resource exists and is available for the selected time.

Main Flow:
1. The user selects a resource and date.
2. The system checks availability.
3. The system creates the booking.
4. The booking is confirmed to the user.

Alternative Flow:
- If the slot is already booked, the system returns an error.

Postconditions:
- A booking record is created.

---

## UC-04: Manage Resources

Actor: Admin

Description:
Allows an administrator to create, update or deactivate resources.

Preconditions:
- The user has admin privileges.

Main Flow:
1. The admin submits resource data.
2. The system validates the input.
3. The resource is stored or updated.
