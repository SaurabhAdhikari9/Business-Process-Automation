using BusinessProcessAutomation.Application.Common.CustomeExceptions;
using System.Net;
using System.Text.Json;

namespace BusinessProcessAutomation.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var jsonResponse = JsonSerializer.Serialize(exception.Message);
            if (statusCode == HttpStatusCode.InternalServerError)
            {
                jsonResponse = JsonSerializer.Serialize("Internal Server Error from the custom middleware.");
            }
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
