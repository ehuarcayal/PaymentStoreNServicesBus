using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transactions.Messages.Events
{
    public class PaymentCompleteEvent : IEvent
    {
        public string PaymentId { get; protected set; }
        public string OrderId { get; protected set; }

        public PaymentCompleteEvent(string paymentId, String orderId)
        {
            PaymentId = paymentId;
            OrderId = orderId;
        }
    }
}
