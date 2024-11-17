using BusinessProcessAutomation.Middlewares;

namespace BusinessProcessAutomation.Extensions
{
    public static class MiddlewareInjections
    {
        public static void MiddlewareInjection(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();   
        }
    }
}
