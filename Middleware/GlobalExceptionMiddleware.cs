using System.Text.Json;
using NutriCook_AI_WebAPI.Middleware.Exceptions;

namespace NutriCook_AI_WebAPI.Middleware
{
    public class GlobalExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                {
                    await _next(context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");

                context.Response.ContentType = "application/json";

                int statusCode = 500;

                if (ex is AppException appEx)
                {
                    statusCode = appEx.StatusCode;
                }

                var response = new
                {
                    success = false,
                    message = ex.Message,
                    StatusCode = statusCode,
                    detail = _env.IsDevelopment() ? ex.StackTrace : null
                };

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response)
                );
            }
        }
    }
}
