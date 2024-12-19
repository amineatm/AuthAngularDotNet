
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;

namespace AuthECApi.Controllers
{
    public static class AuthorizationDemoEndpoints
    {
        public static IEndpointRouteBuilder MapAutorizationDemopoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/AdminOnly", AdminOnly);
            app.MapGet("/AdminAndTeacher", AdminAndTeacher);
            app.MapGet("/LibrarySubscribersOnly", LibrarySubscribersOnly);
            app.MapGet("/IsFemale", OnlyFemale);
            app.MapGet("/OnlyUnder10", OnlyUnder10);
            app.MapGet("/ApplyForMaternityLeave", ApplyForMaternityLeave);
            app.MapGet("/Under10Female", Under10Female);
            return app;
        }

        [Authorize(Policy = "Under10,IsFemale")]
        private static string Under10Female(HttpContext context)
        {
            return ("Welcome Child!");
        }
        [Authorize(Policy = "Under10")]
        private static string OnlyUnder10(HttpContext context)
        {
            return ("Welcome Child!");
        }

        [Authorize(Policy = "IsFemale", Roles = "Teacher")]
        private static string ApplyForMaternityLeave(HttpContext context)
        {
            return ("Welcome to ApplyForMaternityLeave!");
        }

        [Authorize(Policy = "IsFemale")]
        private static string OnlyFemale(HttpContext context)
        {
            return ("Welcome female!");
        }

        [Authorize(Roles = "Admin")]
        private static string AdminOnly(HttpContext context)
        {
            return ("Welcome Admin!");
        }

        [Authorize(Roles = "Admin,Teacher")]
        private static string AdminAndTeacher(HttpContext context)
        {
            return ("Welcome Admin or Teacher!");
        }

        [Authorize(Policy = "HasLibraryID")]
        private static string LibrarySubscribersOnly(HttpContext context)
        {
            return ("Welcome, Library Subscriber!");
        }
    }
}
