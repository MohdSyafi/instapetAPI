using instapetService.ServiceModel;
using instapetService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

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

        [HttpPost("AddPost")]
        public async Task<IActionResult> AddPost([FromForm] PostAndImages post)
        {
            return Ok(await _postService.AddPost(post));
        }
    }
}
