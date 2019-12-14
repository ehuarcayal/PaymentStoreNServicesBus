using System;
using System.Collections.Generic;
using System.Text;

namespace Transactions.Application.Dtos
{
    public class PerformPaymentRequestDto
    {
        public string creditCardId { get; set; }        
        public string orderId { get; set; }
        public decimal Amount { get; set; }        

    }
}
