using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Application.Dto
{
    public class LoginResponseDto : ResponseDto
    {
        public CustomerLoginResponseDto customerData { get; set; }
        
    }
    
}
