namespace Booking.Domain.Entities;

public class Resource : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int Capacity { get; private set; }
    public bool IsActive { get; private set; }

    // Ctor privado para EF
    private Resource() { }

    public Resource(string name, int capacity, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));

        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than 0", nameof(capacity));

        Name = name;
        Capacity = capacity;
        Description = description;
        IsActive = true;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}
