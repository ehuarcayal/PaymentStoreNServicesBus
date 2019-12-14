using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCards.Messages.Events
{
    public class SourceCreditCardNotFountEvent : IEvent
    {
        public string TransactionId { get; protected set; }
        public string OrderId { get; protected set; }

        public SourceCreditCardNotFountEvent(string transactionId, string orderId)
        {
            TransactionId = transactionId;
            OrderId = orderId;
        }
    }
    
}
