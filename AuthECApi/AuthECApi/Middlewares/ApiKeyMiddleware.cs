namespace AuthECApi.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System.Threading.Tasks;

    namespace Jobsmart.Application.Middlewares
    {
        public class ApiKeyMiddleware
        {
            private readonly RequestDelegate _next;

            public ApiKeyMiddleware(RequestDelegate next)
            {
                _next = next ?? throw new ArgumentNullException(nameof(next));
            }

            public async Task InvokeAsync(HttpContext context, IApiKeyValidator apiKeyValidator)
            {
                if (!apiKeyValidator.Validate(context))
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("API Key not found or invalid.");
                }
                else
                {
                    await _next(context);
                }
            }
        }
    }
}
