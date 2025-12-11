using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

// alias para que no choque el nombre Booking
using BookingEntity = Booking.Domain.Entities.Booking;

namespace Booking.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Resource> Resources => Set<Resource>();
    public DbSet<BookingEntity> Bookings => Set<BookingEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Para este MVP/portfolio NO vamos a usar FKs reales a User/Resource.
        // Así evitamos el error de FOREIGN KEY cuando mandamos cualquier Guid.

        modelBuilder.Entity<BookingEntity>(entity =>
        {
            // Le decimos a EF que NO mapee las propiedades de navegación,
            // sólo las columnas UserId / ResourceId.
            entity.Ignore(b => b.User);
            entity.Ignore(b => b.Resource);
        });
    }
}
