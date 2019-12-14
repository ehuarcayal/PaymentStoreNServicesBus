using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCards.Messages.Events
{
    public class WithdrawMoneyRejectedEvent : IEvent
    {
        public string TransactionId { get; protected set; }
        public string OrderId { get; protected set; }

        public WithdrawMoneyRejectedEvent(string transactionId, string orderId)
        {
            TransactionId = transactionId;
            OrderId = orderId;
        }
    }
}
