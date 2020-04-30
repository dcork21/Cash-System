using System;
using CashSystemMVC.Interfaces.System;
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
    public class WithdrawController : ControllerBase
    {
        private readonly IRequestWithdraw _requestWithdraw;

        public WithdrawController(IRequestWithdraw requestWithdraw)
        {
            _requestWithdraw = requestWithdraw;
        }

        [HttpPost]
        public IActionResult Create([FromBody] WithdrawalRequest withdrawal)
        {
            try
            {
                string withdrawalToken = _requestWithdraw
                    .CreateWithdraw(withdrawal.SessionToken, withdrawal.AccountId, withdrawal.Amount);

                if (string.IsNullOrEmpty(withdrawalToken)) return new UnauthorizedResult();

                return new OkObjectResult(new WithdrawalResponse() { AccountId = withdrawal.AccountId, WithdrawalToken = withdrawalToken, Expiry = DateTime.Now.AddMinutes(20)});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Verify([FromBody] WithdrawalResponse withdrawal)
        {
            try
            {
                Withdrawal verifiedWithdrawal = _requestWithdraw.VerifyWithdraw(withdrawal.AccountId, withdrawal.WithdrawalToken);

                if (verifiedWithdrawal == null) return new UnauthorizedResult();

                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Confirm([FromBody] WithdrawalResponse withdrawalResponse)
        {
            try
            {
                if (!_requestWithdraw.ConfirmWithdraw(withdrawalResponse.AccountId, withdrawalResponse.WithdrawalToken)) 
                    return new BadRequestResult();
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }
    }
}