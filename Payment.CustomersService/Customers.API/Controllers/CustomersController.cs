
using System;
using System.Collections.Generic;
using Customers.Application;
using Customers.Application.Contracts;
using Customers.Application.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpgFisi.Common.Domain;
using ResponseDto = Customers.Application.ResponseDto;

namespace Customers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerQueries _customerQueries;
        private readonly ICustomerApplicationService _customerApplicationService;

        public CustomersController(ICustomerQueries customerQueries, ICustomerApplicationService customerService)
        {
            _customerQueries = customerQueries;
            _customerApplicationService = customerService;
        }

        // GET api/customers
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("list")]
        public IActionResult GetListPaginated([FromQuery]int page = 0, [FromQuery]int pageSize = 10)
        {
            try
            {
                List<CustomerDto> customers = _customerQueries.GetListPaginated(page, pageSize);
                return StatusCode(StatusCodes.Status200OK, customers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new UpgFisi.Common.Domain.ApiStringResponse("Internal Server Error"));
            }
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public IActionResult Get(String id)
        {
            try
            {
                CustomerDto customers = _customerQueries.getCustomer(id);
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
        public IActionResult Post([FromBody] NewCustomerDto newCustomerDto)
        {
            newCustomerDto.CustomerId = Guid.NewGuid().ToString();
            ResponseDto response = _customerApplicationService.Register(newCustomerDto);
            return StatusCode(response.HttpStatusCode, response.Response);
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
