using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using WebApplication1.Models;

namespace WebApplication1.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var errorMessage = "An unexpected error occurred.";
            var errorDetailsMessage = exception.Message;

            var sqlException = exception.GetBaseException() as SqlException;
            // Check for SQL exceptions
            if (sqlException != null)
            {
                errorDetailsMessage = sqlException.Message;
                // Map SQL error codes to friendly error messages
                switch (sqlException.Number)
                {
                    case 2: // Connection Timeout
                    case -2: // Connection Timeout (another code)
                        errorMessage = "The database operation timed out. Please try again later.";
                        statusCode = HttpStatusCode.RequestTimeout;
                        break;
                    case 547: // Foreign Key violation
                        errorMessage = "Operation failed due to a reference constraint.";
                        statusCode = HttpStatusCode.BadRequest;
                        break;
                    case 1105: // Insufficient Disk Space
                        errorMessage = "The database server is out of disk space.";
                        statusCode = HttpStatusCode.InsufficientStorage;
                        break;
                    case 1205: // Deadlock Victim
                        errorMessage = "The request was terminated due to a database deadlock.";
                        statusCode = HttpStatusCode.Conflict;
                        break;
                    case 2601: // Unique Index/Constraint Violation
                    case 2627: // Unique Constraint Violation
                        errorMessage = "A record with the same unique identifier already exists.";
                        statusCode = HttpStatusCode.Conflict;
                        break;
                    case 4060: // Invalid Database
                        errorMessage = "The database requested is unavailable.";
                        statusCode = HttpStatusCode.ServiceUnavailable;
                        break;
                    case 18456: // Login Failed
                        errorMessage = "Invalid database credentials provided.";
                        statusCode = HttpStatusCode.Unauthorized;
                        break;
                    case 207: // Invalid Column Name
                        errorMessage = "The operation failed due to an invalid column name in the query.";
                        statusCode = HttpStatusCode.BadRequest;
                        break;
                    case 208: // Invalid Object Name
                        errorMessage = "The operation failed because the referenced table or view does not exist.";
                        statusCode = HttpStatusCode.BadRequest;
                        break;
                    case 229: // Permission Denied
                        errorMessage = "You do not have permission to perform this operation on the database.";
                        statusCode = HttpStatusCode.Forbidden;
                        break;
                    case 8152: // String or binary data would be truncated
                        errorMessage = "Data truncation error: One or more fields exceed the defined size.";
                        statusCode = HttpStatusCode.BadRequest;
                        break;
                    case 53: // Network-related or instance-specific error
                        errorMessage = "The database server is not reachable. Please check your network connection or server status.";
                        statusCode = HttpStatusCode.ServiceUnavailable;
                        break;
                    default:
                        errorMessage = "A database error occurred.";
                        break;
                }
            }

            var errorDetails = new ErrorDetails
            {
                StatusCode = (int)statusCode,
                Message = errorMessage,
                Details = errorDetailsMessage
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorDetails));
        }
    }
}
