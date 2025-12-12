using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Tomamos el connection string del appsettings
        var connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=booking.db"; // fallback local

        // SQLite
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        // Repositorios
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IResourceRepository, ResourceRepository>();

        return services;
    }
}
