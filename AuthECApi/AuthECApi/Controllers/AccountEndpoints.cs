using AuthECApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AuthECAPI.Controllers
{
    public static class AccountEndpoints
    {
        public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/UserProfile", GetUserProfile);
            app.MapGet("/UserRoles", GetUserRoles);
            return app;
        }

        [AllowAnonymous]
        private static async Task<IResult> GetUserRoles(AppDbContext dbContext)
        {
            var roleList = await dbContext.Roles.Select(x => x.Name).ToListAsync();
            if (roleList == null || !roleList.Any())
                return Results.NotFound("No roles found.");

            return Results.Ok(roleList);
        }

        [Authorize]
        private static async Task<IResult> GetUserProfile(
            ClaimsPrincipal user,
            UserManager<AppUser> userManager)
        {
            // Ensure the claim name used is consistent with your token
            var userID = user.Claims.FirstOrDefault(x => x.Type == "userID")?.Value;

            if (userID == null)
                return Results.Unauthorized();

            var userDetails = await userManager.FindByIdAsync(userID);

            if (userDetails == null)
                return Results.NotFound("User not found.");

            return Results.Ok(new
            {
                Email = userDetails.Email,
                FullName = userDetails.FullName
            });
        }
    }
}
