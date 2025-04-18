namespace DotNetCore_CRUD_API.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static Dictionary<string, DateTime> _clients = new();

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            if (ip != null && _clients.ContainsKey(ip))
            {
                var lastRequestTime = _clients[ip];
                if ((DateTime.Now - lastRequestTime).TotalSeconds < 5)
                {
                    context.Response.StatusCode = 429;
                    await context.Response.WriteAsync("Too many requests. Please wait.");
                    return;
                }
            }

            _clients[ip!] = DateTime.Now;
            await _next(context);
        }
    }

}
