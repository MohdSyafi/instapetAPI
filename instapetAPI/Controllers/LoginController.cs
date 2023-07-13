
using instapetService.Interfaces;
using instapetService.Models;
using Microsoft.AspNetCore.Mvc;

namespace instapetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILoginService _LoginService;

        public LoginController(ILoginService loginService)
        {
            _LoginService = loginService;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User requestModel)
        {
            try
            {      
                return Ok(await _LoginService.Login(requestModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("Signup")]
        public async Task<IActionResult>  Signup([FromBody] User requestModel)
        {
            try
            {
                return Ok( await _LoginService.Signup(requestModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
