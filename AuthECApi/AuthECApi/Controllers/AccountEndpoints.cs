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
            return Results.Ok(roleList);
        }

        [Authorize]
        private static async Task<IResult> GetUserProfile(
          ClaimsPrincipal user,
          UserManager<AppUser> userManager)
        {
            var userID = user.Claims.First(x => x.Type == "userID").Value;
            var userDetails = await userManager.FindByIdAsync(userID);
            return Results.Ok(
              new
              {
                  Email = userDetails?.Email,
                  FullName = userDetails?.FullName,
              });
        }
    }
}