using instapetService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace instapetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : Controller
    {

        public readonly IProfileService _profileService;

        public ProfileController( IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            return Ok(await _profileService.GetProfile(userId));
        }
    }
}
