using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCards.Application.Dto
{
    public class NewCreditCardDto
    {
        public virtual string creditCardId { get; set; }
        public virtual string numberCard { get; set; }
        public virtual string CustomerId { get; set; }
        public virtual string type { get; set; }
        public virtual DateTime expiration { get; set; }
        public Decimal mount { get; set; }
        public string ccv { get; set; }      

        public NewCreditCardDto()
        {
        }

    }
}
