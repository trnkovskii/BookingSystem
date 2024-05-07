using Newtonsoft.Json;
using System.Net;

namespace BookingSystem.WebAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly string generalErrorMessage = "Oops, something went wrong. Please try again later or report the problem to the adimistrator.";
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            if (exception is KeyNotFoundException || exception is EntryPointNotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
            else if (exception is ArgumentException || exception is ArgumentNullException) code = HttpStatusCode.BadRequest;
            else if (exception is ArgumentOutOfRangeException) code = HttpStatusCode.RequestedRangeNotSatisfiable;

            var result = JsonConvert.SerializeObject(new { error = generalErrorMessage });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
