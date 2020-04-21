using System;
using CashSystemMVC.Interfaces.Business;
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
                var user = _userMgt.CreateUser(userName, password, firstName, lastName, address, postcode, mobile,
                    email);

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
                var user = _userMgt.GetUser(loginRequest.UserName, loginRequest.Password);

                if (user == null) return new UnauthorizedResult();

                return new OkObjectResult(new LoginResponse
                {
                    UserId = user.UserId, 
                    SessionToken = user.SessionToken, 
                    UserName = user.UserIdentity.UserName,
                    FirstName = user.UserIdentity.FirstName,
                    LastName = user.UserIdentity.LastName,
                    PostAddress = user.UserIdentity.PostAddress,
                    Postcode = user.UserIdentity.Postcode,
                    Mobile = user.UserIdentity.Mobile,
                    Email = user.UserIdentity.Email,
                    Accounts = user.Accounts
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }
    }
}