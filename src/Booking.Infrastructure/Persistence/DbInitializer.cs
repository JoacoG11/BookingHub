using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Aseguramos que la BD y las tablas existan
        await context.Database.EnsureCreatedAsync();

        // Si ya hay datos, no hacemos nada (evita duplicar)
        if (await context.Users.AnyAsync() || await context.Resources.AnyAsync())
            return;

        // Creamos un usuario y un recurso de prueba
        var demoUser = new User("Demo User", "demo@example.com");
        var demoResource = new Resource("Demo Room", 10, "Resource for testing bookings");

        await context.Users.AddAsync(demoUser);
        await context.Resources.AddAsync(demoResource);
        await context.SaveChangesAsync();

        // Logueamos los IDs para usarlos en Swagger
        Console.WriteLine("============== DB SEED ==============");
        Console.WriteLine($"UserId seeded:     {demoUser.Id}");
        Console.WriteLine($"ResourceId seeded: {demoResource.Id}");
        Console.WriteLine("=====================================");
    }
}
