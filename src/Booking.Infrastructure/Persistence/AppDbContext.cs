using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Booking.Domain.Entities.Booking> Bookings => Set<Booking.Domain.Entities.Booking>();
    public DbSet<Resource> Resources => Set<Resource>();
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Booking.Domain.Entities.Booking>(b =>
        {
            b.HasKey(x => x.Id);

            b.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            b.HasOne(x => x.Resource)
                .WithMany()
                .HasForeignKey(x => x.ResourceId);
        });

        modelBuilder.Entity<Resource>(r =>
        {
            r.HasKey(x => x.Id);
        });

        modelBuilder.Entity<User>(u =>
        {
            u.HasKey(x => x.Id);
        });
    }
}
