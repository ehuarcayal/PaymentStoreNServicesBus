using CreditCards.Application.Contracts;
using CreditCards.Application.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UpgFisi.Common.Domain;

namespace CreditCards.API.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CreditCardsController : ControllerBase
    {                
        private readonly ICreditCardQueries _creditCardQueries;
        private readonly ICreditCardApplicationService _creditCardApplicationService;

        public CreditCardsController(ICreditCardQueries creditCardQueries,
            ICreditCardApplicationService creditCardrService)
        {            
            _creditCardQueries = creditCardQueries;
            _creditCardApplicationService = creditCardrService;
        }


        [HttpGet("customer/{id}")]
        public IActionResult Get(String id)
        {
            try
            {
                List<CreditCardDto> customers = _creditCardQueries.GetListBycustomer(id);
                return StatusCode(StatusCodes.Status200OK, customers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiStringResponse("Internal Server Error"));
            }
        }

        // POST api/customers
        [HttpPost]
        public IActionResult Post([FromBody] NewCreditCardDto newCreditCardDto)
        {
            newCreditCardDto.creditCardId = Guid.NewGuid().ToString();
            ResponseDto response = _creditCardApplicationService.Register(newCreditCardDto);
            return StatusCode(response.HttpStatusCode, response.Response);
        }
    }
}
