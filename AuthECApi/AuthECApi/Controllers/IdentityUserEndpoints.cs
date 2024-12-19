using AuthECApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthECApi.Controllers
{
    public static class IdentityUserEndpoints
    {
        public static IEndpointRouteBuilder MapIdentityUserEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/signup", CreateUser);

            app.MapPost("/signin", SignIn);

            return app;
        }
        private static async Task<IResult> CreateUser(UserManager<AppUser> userManager, [FromBody] UserRegistrationModel userRegistrationModel)
        {
            AppUser user = new AppUser()
            {
                UserName = userRegistrationModel.Email,
                Email = userRegistrationModel.Email,
                FullName = userRegistrationModel.FullName,
            };
            var result = await userManager.CreateAsync(user, userRegistrationModel.Password);

            if (result.Succeeded)
                return Results.Ok(result);
            else
                return Results.BadRequest(result);
        }
        private static async Task<IResult> SignIn(UserManager<AppUser> userManager, [FromBody] UserLoginModel userRegistrationModel, IOptions<AppSettings> appSettings)
        {
            var user = await userManager.FindByEmailAsync(userRegistrationModel.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, userRegistrationModel.Password))
            {
                var SigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Value.JWTSecret));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("userID", user.Id.ToString())
                    }),
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
