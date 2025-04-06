
using Domain.Enums;
using Presentation.ViewModels.ResponseVM;
using System.Net;
using System.Text.Json;

namespace Presentation.MiddleWares
{
    public class GlobalExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Continue request pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        // Handles exceptions and returns a standardized error response.
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new ResponseViewModel<string>
            {
                Data = null,
                Message = exception.Message,
                IsSuccess = false,
                StatusCode = ErrorCodeEnum.InternalServerError
            };
            // Convert response object to JSON format
            string jsonResponse = JsonSerializer.Serialize(errorResponse);
            // Set HTTP status code to 500 (Internal Server Error)
            response.StatusCode = (int)ErrorCodeEnum.InternalServerError;
            // Write the JSON response to the HTTP response body
            return response.WriteAsync(jsonResponse);
        }

    }
}
