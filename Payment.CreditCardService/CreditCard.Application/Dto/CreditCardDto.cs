using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCards.Application.Dto
{
    public class CreditCardDto
    {
        public string id { get; set; }
        public string numberCard { get; set; }
        public string customerId { get; set; }
        public DateTime expirationDate { get; set; }        

        public CreditCardDto()
        {
        }
    }
}
