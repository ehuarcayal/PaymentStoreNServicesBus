using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transactions.Messages.Commands
{
    public class RequestPaymentCommand : ICommand
    {
        public string TransactionId { get; private set; }
        public string CreditCardId { get; private set; }
        public string OrderId { get; private set; }
        public decimal Amount { get; private set; }        

        public RequestPaymentCommand(string transactionId, string creditCardId, String orderId, decimal amount)
        {
            TransactionId = transactionId;
            CreditCardId = creditCardId;
            OrderId = orderId;
            Amount = amount;            
        }
    }
}
