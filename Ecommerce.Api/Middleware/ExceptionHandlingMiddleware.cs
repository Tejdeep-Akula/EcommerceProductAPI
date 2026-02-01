using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            await WriteErrorResponse(context, 404, ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            await WriteErrorResponse(context, 400, ex.Message);
        }
        catch (ArgumentException ex)
        {

            await WriteErrorResponse(context, 400, ex.Message);
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogWarning(ex,"Request was cancelled.");
            await WriteErrorResponse(context, 499, "Request was cancelled");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, "A concurrency error occurred.");
            await WriteErrorResponse(context, 409, "A concurrency error occurred. Please retry the operation.");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "A database update error occurred.");
            await WriteErrorResponse(context, 500, "A database error occurred. Please contact support.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await WriteErrorResponse(context, 500, "Internal server error");
        }
    }

    private static async Task WriteErrorResponse(
        HttpContext context,
        int statusCode,
        string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            statusCode,
            message
        });
    }
}
