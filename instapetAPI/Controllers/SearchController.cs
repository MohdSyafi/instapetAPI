using instapetService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace instapetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {

        public readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost("SearchUser")]
        public async Task<IActionResult> SearchUser( string input,int UserId)
        {
            return Ok( await _searchService.SearchUser(input,UserId));
        }
    }
}
