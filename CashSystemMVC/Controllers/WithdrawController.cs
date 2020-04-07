using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CashSystemMVC.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors]
    public class WithdrawController : ControllerBase
    {
        public WithdrawController()
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody] object withdrawRequest)
        {
            return new AcceptedResult();
        }
    }
}