
using Microsoft.AspNetCore.Authorization;

namespace AuthECApi.Controllers
{
    public static class AccountEndpoints
    {
        public static IEndpointRouteBuilder MapAccountEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/UserProfile", GetUserProfile);

            return app;
        }
        [Authorize]

        private static string GetUserProfile()
        {
            return "User Profile";
        }
    }
}
