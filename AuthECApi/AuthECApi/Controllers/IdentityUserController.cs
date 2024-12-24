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
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityUserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IOptions<AppSettings> _appSettings;

        public IdentityUserController(UserManager<AppUser> userManager, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> CreateUser([FromBody] UserRegistrationModel userRegistrationModel)
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

            var result = await _userManager.CreateAsync(user, userRegistrationModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, userRegistrationModel.Role);
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] UserLoginModel userLoginModel)
        {
            var user = await _userManager.FindByEmailAsync(userLoginModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, userLoginModel.Password))
            {
                var role = await _userManager.GetRolesAsync(user);
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Value.JWTSecret));

                ClaimsIdentity claims = new(
                [
                    new Claim("userID", user.Id.ToString()),
                    new Claim("gender", user.Gender.ToString()),
                    new Claim("age", (DateTime.Now.Year - user.DOB.Year).ToString()),
                    new Claim(ClaimTypes.Role, role.First())
                ]);

                if (user.LibraryID != null)
                {
                    claims.AddClaim(new Claim("libraryID", user.LibraryID.ToString()!));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect!" });
            }
        }
    }
}
