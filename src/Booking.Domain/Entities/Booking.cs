namespace Booking.Domain.Entities;

public class Booking : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid ResourceId { get; private set; }

    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    public BookingStatus Status { get; private set; }

    // Navegación
    public User? User { get; private set; }
    public Resource? Resource { get; private set; }

    private Booking() { }

    public Booking(Guid userId, Guid resourceId, DateTime startTime, DateTime endTime)
    {
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be before end time.");

        UserId = userId;
        ResourceId = resourceId;
        StartTime = startTime;
        EndTime = endTime;
        Status = BookingStatus.Pending;
    }

    public void Confirm()
    {
        if (Status == BookingStatus.Cancelled)
            throw new InvalidOperationException("Cannot confirm a cancelled booking.");

        Status = BookingStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == BookingStatus.Cancelled)
            return;

        Status = BookingStatus.Cancelled;
    }

    public void Reschedule(DateTime newStart, DateTime newEnd)
    {
        if (newStart >= newEnd)
            throw new ArgumentException("Start time must be before end time.");

        if (Status == BookingStatus.Cancelled)
            throw new InvalidOperationException("Cannot reschedule a cancelled booking.");

        StartTime = newStart;
        EndTime = newEnd;
    }
}
