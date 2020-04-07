using CashSystemMVC.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CashSystemMVC.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors]
    public class LoginController : ControllerBase
    {
        private const string UserName = "User";
        private const string Password = "Password1";
        private const string LoginToken = "shbSJhdblRWVr24W187AW3f";


        public LoginController()
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest loginRequest)
        {
            if(string.IsNullOrEmpty(loginRequest.UserName) && string.IsNullOrEmpty(loginRequest.Password))
                return new BadRequestObjectResult(new LoginResponse() { Token = "Unauthorized" });
            if(loginRequest.LoginToken.Equals(LoginToken) || (loginRequest.UserName.Equals(UserName) && loginRequest.Password.Equals(Password)))
                return new OkObjectResult(new LoginResponse(){Username = UserName, Token = LoginToken });

            return new UnauthorizedObjectResult(new LoginResponse() { Token = "Unauthorized" });
        }
    }
}