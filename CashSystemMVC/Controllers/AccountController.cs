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
    public class AccountController : ControllerBase
    {
        private readonly IAccountMgt _accountMgt;

        public AccountController(IAccountMgt accountMgt)
        {
            _accountMgt = accountMgt;
        }

        [HttpPut]
        public IActionResult RegisterAccount(string sessionToken, int userId, string accountNumber, string sortCode, float balance)
        {
            try
            {
                Account account = _accountMgt.CreateAccount(sessionToken, userId, accountNumber,sortCode, balance);

                if (account == null) return new BadRequestResult();

                return new OkObjectResult(account);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }

        }
    }
}