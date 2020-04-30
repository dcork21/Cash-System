using System;
using CashSystemMVC.Interfaces.Business;
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
    public class UdpTestController : ControllerBase
    {
        private readonly IUdpClient _udpClient;

        public UdpTestController(IUdpClient udpClient)
        {
            _udpClient = udpClient;
        }

        [HttpPut]
        public IActionResult TestUdp( int port, string message)
        {
            try
            {
                _udpClient.SendMessageToAtm(port, message);
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