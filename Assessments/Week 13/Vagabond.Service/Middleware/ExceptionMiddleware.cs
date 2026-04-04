using Vagabond.Service.Exceptions;

namespace Vagabond.Service.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DestinationNotFoundException ex)
            {
                context.Response.StatusCode = 404;

                await context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = 404,
                    Message = ex.Message
                });
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = 500,
                    Message = "Internal Server Error"
                });
            }
        }
    }
}