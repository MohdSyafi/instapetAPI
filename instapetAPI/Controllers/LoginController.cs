using instapetService.Services;
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
        public IActionResult Authenticate([FromBody] LoginRequestModel requestModel)
        {
            try
            {
               
                return Ok(_LoginService.Login(requestModel.Username, requestModel.Password));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public class LoginRequestModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
