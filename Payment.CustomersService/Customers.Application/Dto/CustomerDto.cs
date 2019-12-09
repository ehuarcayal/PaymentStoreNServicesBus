using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Application.Dto
{
    public class CustomerDto
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string identityDocument { get; set; }
        public bool active { get; set; }

        public CustomerDto()
        {
        }
    }
}
