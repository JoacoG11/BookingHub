using Microsoft.Extensions.DependencyInjection;
using Booking.Application.Bookings;

namespace Booking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registramos los servicios de aplicación aquí
        services.AddScoped<IBookingService, BookingService>();

        return services;
    }
}
