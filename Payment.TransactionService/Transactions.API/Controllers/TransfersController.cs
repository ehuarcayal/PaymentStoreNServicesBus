using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Transactions.Application;
using Transactions.Application.Dtos;

namespace Transactions.API.Controllers
{
    [Route("api/Payment")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class TransfersController : ControllerBase
    {
        private readonly ITransactionApplicationService _transactionApplicationService;

        public TransfersController(ITransactionApplicationService transactionApplicationService)
        {
            _transactionApplicationService = transactionApplicationService;
        }

        // POST api/transfers
  /*      [HttpPost]
        public async Task<IActionResult> PerformMoneyTransfer([FromBody] PerformMoneyTransferRequestDto dto)
        {
            PerformMoneyTransferResponseDto response = await _transactionApplicationService.PerformTransfer(dto);
            return Ok(response);
        }
*/
        // GET api/transfers
        [HttpGet]
        public IActionResult Home()
        {            
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PerformPayment ([FromBody] PerformPaymentRequestDto dto)
        {
            PerformMoneyTransferResponseDto response = await _transactionApplicationService.PerformPayment(dto);
            return Ok(response);
        }
    }
}
