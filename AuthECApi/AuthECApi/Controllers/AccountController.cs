using AuthECApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;

        public AccountController(UserManager<AppUser> userManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet("UserRoles")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserRoles()
        {
            var roleList = await _dbContext.Roles.Select(x => x.Name).ToListAsync();
            if (roleList == null || !roleList.Any())
                return NotFound("No roles found.");

            return Ok(roleList);
        }

        [HttpGet("UserProfile")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            var userID = User.Claims.FirstOrDefault(x => x.Type == "userID")?.Value;

            if (userID == null)
                return Unauthorized();

            var userDetails = await _userManager.FindByIdAsync(userID);

            if (userDetails == null)
                return NotFound("User not found.");

            return Ok(new UserProfileResponseModel
            {
                Email = userDetails.Email,
                FullName = userDetails.FullName
            });
        }
    }
}
