using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthECApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("AdminOnly")]
        public IActionResult AdminOnly()
        {
            return Ok("Welcome Admin!");
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("AdminAndTeacher")]
        public IActionResult AdminAndTeacher()
        {
            return Ok("Welcome Admin or Teacher!");
        }

        [Authorize(Policy = "HasLibraryID")]
        [HttpGet("LibrarySubscribersOnly")]
        public IActionResult LibrarySubscribersOnly()
        {
            return Ok("Welcome, Library Subscriber!");
        }

        [Authorize(Policy = "IsFemale")]
        [HttpGet("IsFemale")]
        public IActionResult OnlyFemale()
        {
            return Ok("Welcome female!");
        }

        [Authorize(Policy = "Under10")]
        [HttpGet("OnlyUnder10")]
        public IActionResult OnlyUnder10()
        {
            return Ok("Welcome Child!");
        }

        [Authorize(Policy = "IsFemale", Roles = "Teacher")]
        [HttpGet("ApplyForMaternityLeave")]
        public IActionResult ApplyForMaternityLeave()
        {
            return Ok("Welcome to ApplyForMaternityLeave!");
        }

        [Authorize(Policy = "Under10,IsFemale")]
        [HttpGet("Under10Female")]
        public IActionResult Under10Female()
        {
            return Ok("Welcome Child!");
        }
    }
}
