using System;
using CashSystemMVC.Interfaces.Business;
using CashSystemMVC.Models;
using CashSystemMVC.Models.Requests;
using CashSystemMVC.Models.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CashSystemMVC.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors]
    public class UserController : ControllerBase
    {
        private readonly IUserMgt _userMgt;

        public UserController(IUserMgt userMgt)
        {
            _userMgt = userMgt;
        }

        [HttpPut]
        public IActionResult Register(
            string userName,
            string password,
            string firstName,
            string lastName,
            string address,
            string postcode,
            string mobile,
            string email)
        {
            try
            {
                User user = _userMgt.CreateUser(userName, password, firstName, lastName, address, postcode, mobile, email);

                if (user == null) return new BadRequestResult();

                return new OkResult();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }

        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] LoginRequest loginRequest)
        {
            try
            {
                User user = _userMgt.GetUser(loginRequest.UserName, loginRequest.Password);

                if(user == null) return new UnauthorizedResult();

                return new OkObjectResult(new LoginResponse(){UserId = user.UserId, SessionToken = user.SessionToken});

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }

        }
    }
}