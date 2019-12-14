using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transactions.Messages.Events
{
    public class PaymentRequestedEvent : IEvent
    {
        public string TransactionId { get; protected set; }
        public string CreditCardId { get; protected set; }
        public string OrderId { get; private set; }
        public decimal Amount { get; protected set; }        

        public PaymentRequestedEvent(string transactionId, string creditCardId, decimal amount, String orderId)
        {
            TransactionId = transactionId;
            CreditCardId = creditCardId;            
            Amount = amount;
            OrderId = orderId;
        }
    }
}
