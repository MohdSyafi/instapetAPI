using instapetService.Interfaces;
using instapetService.ServiceModel;
using Microsoft.AspNetCore.Mvc;


namespace instapetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("GetPosts")]
        public async Task<IActionResult> GetPosts(int userId)
        {
            return Ok(await _postService.GetPosts(userId));
        }

        [HttpPost("AddPost")]
        public async Task<IActionResult> AddPost([FromForm] PostAndImages post)
        {
            return Ok(await _postService.AddPost(post));
        }
    }
}
