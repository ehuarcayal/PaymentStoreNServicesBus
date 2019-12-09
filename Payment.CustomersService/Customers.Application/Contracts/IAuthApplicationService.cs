using Customers.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Application.Contracts
{
    public interface IAuthApplicationService
    {
        LoginResponseDto Login(LoginDto loginDto);
    }
}
