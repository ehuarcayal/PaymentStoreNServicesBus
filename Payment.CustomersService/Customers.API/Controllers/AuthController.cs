﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers.Application.Contracts;
using Customers.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Customers.API.Controllers
{
    [Route("/api/users")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthApplicationService _authApplicationService;

        public AuthController(IAuthApplicationService authApplicationService)
        {
            _authApplicationService = authApplicationService;            
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            LoginResponseDto response = _authApplicationService.Login(loginDto);
            return StatusCode(response.HttpStatusCode, response.Response);
        }
    }
}