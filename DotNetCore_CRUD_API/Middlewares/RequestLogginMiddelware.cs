namespace DotNetCore_CRUD_API.Middlewares
{
    public class RequestLogginMiddelware
    {
        private readonly RequestDelegate _delegate;

        public RequestLogginMiddelware(RequestDelegate @delegate)
        {
            _delegate = @delegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var method = httpContext.Request.Method;
            var path = httpContext.Request.Path;
            Console.WriteLine($"Request: {method} {path}");
            await _delegate(httpContext);
            var statusCode = httpContext.Response.StatusCode;
            Console.WriteLine($"Response: {statusCode}");
        }
    }
}
