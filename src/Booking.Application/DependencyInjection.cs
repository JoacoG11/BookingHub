using Booking.Application.Bookings;
using Booking.Application.Resources;
using Booking.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Booking
        services.AddScoped<IBookingService, BookingService>();

        // Resources
        services.AddScoped<IResourceService, ResourceService>();

        // Users
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
