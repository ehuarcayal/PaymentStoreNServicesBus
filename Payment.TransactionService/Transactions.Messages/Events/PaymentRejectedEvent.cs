using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transactions.Messages.Events
{
    public class PaymentRejectedEvent : IEvent
    {
        public string TransferId { get; protected set; }
        public string OrderId { get; protected set; }

        public PaymentRejectedEvent(string transferId, string orderId)
        {
            TransferId = transferId;
            OrderId = orderId;
        }
    }
}
