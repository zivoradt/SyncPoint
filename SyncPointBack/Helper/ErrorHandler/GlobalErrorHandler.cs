using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SyncPointBack.Helper.ErrorHandler.CustomException;
using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SyncPointBack.Helper.ErrorHandler
{
    public class GlobalErrorHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalErrorHandler> _logger;

        public GlobalErrorHandler(ILogger<GlobalErrorHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // Log the exception
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            // Create problem details for the exception
            var problemDetails = new ProblemDetails
            {
                Detail = $"API Error: {exception.Message}",
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "API Error",
                Instance = "API",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
            };

            // Customize handling for specific exception types
            if (exception is NotFoundException)
            {
                problemDetails.Status = (int)HttpStatusCode.NotFound;
            }
            // Add more custom handling for other specific exception types if needed

            // Write problem details to the response
            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails), cancellationToken);

            return true;
        }
    }
}