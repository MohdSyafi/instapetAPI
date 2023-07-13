using instapetService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace instapetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FollowController : Controller
    {

        public readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

        [HttpPost("FollowUser")]
        public async Task<IActionResult>  FollowUser(int id,int idToFollow)
        {
            return Ok( await _followService.FollowUser(id,idToFollow));
        }

        [HttpPost("UnFollowUser")]
        public async Task<IActionResult> UnFollowUser(int id, int idToUnFollow)
        {
            return Ok(await _followService.UnFollowUser(id, idToUnFollow));
        }
    }
}
