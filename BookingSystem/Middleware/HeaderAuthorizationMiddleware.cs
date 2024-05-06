namespace BookingSystem.WebAPI.Middleware
{
    public class HeaderAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string apiKey = context.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(apiKey) || apiKey != "ValidUser")
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Unauthorized access");
                return;
            }

            await _next(context);
        }
    }
}
