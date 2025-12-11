namespace Booking.Domain.Entities;

public class User : BaseEntity
{
    // Propiedades básicas de ejemplo
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    // Ctor privado para EF
    private User() { }

    public User(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));

        Name = name;
        Email = email;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));

        Name = name;
    }
}
