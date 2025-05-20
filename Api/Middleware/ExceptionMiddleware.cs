using Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace Api.Middleware
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new { error = exception.Message });
                    break;
                case ArgumentException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { error = exception.Message });
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
