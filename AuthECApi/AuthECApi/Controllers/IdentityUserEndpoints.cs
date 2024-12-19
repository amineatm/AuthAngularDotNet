using AuthECApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthECApi.Controllers
{
    public static class AIdentityUserEndpoints
    {
        public static IEndpointRouteBuilder MapIdentityUserEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/signup", CreateUser);

            app.MapPost("/signin", SignIn);

            return app;
        }

        [AllowAnonymous]
        private static async Task<IResult> CreateUser(UserManager<AppUser> userManager, [FromBody] UserRegistrationModel userRegistrationModel)
        {
            AppUser user = new AppUser()
            {
                FullName = userRegistrationModel.FullName,
                DOB = DateOnly.FromDateTime(DateTime.Now.AddYears(-userRegistrationModel.Age)),
                Email = userRegistrationModel.Email,
                UserName = userRegistrationModel.Email,
                Gender = userRegistrationModel.Gender,
                LibraryID = userRegistrationModel.LibraryID,
            };
            var result = await userManager.CreateAsync(user, userRegistrationModel.Password);

            await userManager.AddToRoleAsync(user, userRegistrationModel.Role);

            if (result.Succeeded)
                return Results.Ok(result);
            else
                return Results.BadRequest(result);
        }

        [AllowAnonymous]
        private static async Task<IResult> SignIn(UserManager<AppUser> userManager, [FromBody] UserLoginModel userRegistrationModel, IOptions<AppSettings> appSettings)
        {
            var user = await userManager.FindByEmailAsync(userRegistrationModel.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, userRegistrationModel.Password))
            {
                var role = await userManager.GetRolesAsync(user);
                var SigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Value.JWTSecret));

                ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
                {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim("Gender", user.Gender.ToString()),
                        new Claim("Age", (DateTime.Now.Year - user.DOB.Year).ToString()),
                        new Claim(ClaimTypes.Role, role.First()),
                });

                if (user.LibraryID != null)
                {
                    claims.AddClaim(new Claim("LibraryID", user.LibraryID.ToString()!));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(
                        SigningKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Results.Ok(new { token });
            }
            else return Results.BadRequest(new { message = "Username or password is inccorect!" });
        }
    }
}
