using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Application.Dto
{
    public class LoginDto
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public LoginDto()
        {
        }
    }
}
