using System.Net;
using System.Text.Json;

namespace Booking.Api.Middleware;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (KeyNotFoundException ex)
        {
            await WriteErrorAsync(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            // Ej: "Resource is already booked..." -> 409 Conflict
            await WriteErrorAsync(context, HttpStatusCode.Conflict, ex.Message);
        }
        catch (ArgumentException ex)
        {
            await WriteErrorAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception)
        {
            await WriteErrorAsync(context, HttpStatusCode.InternalServerError, "Unexpected error.");
        }
    }

    private static async Task WriteErrorAsync(HttpContext context, HttpStatusCode status, string message)
    {
        context.Response.StatusCode = (int)status;
        context.Response.ContentType = "application/json";

        var payload = JsonSerializer.Serialize(new { error = message });
        await context.Response.WriteAsync(payload);
    }
}
