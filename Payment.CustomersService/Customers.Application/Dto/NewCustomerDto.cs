using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Application.Dto
{
    public class NewCustomerDto
    {
        public virtual string CustomerId { get; set; }
        public virtual string user { get; set; }
        public virtual string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string identityDocument { get; set; }

        public NewCustomerDto()
        {
        }
    }
}
