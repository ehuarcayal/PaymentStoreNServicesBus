using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transactions.Messages.Commands
{
    public class CompletePaymentCommand : ICommand
    {
        public string PaymentId { get; private set; }
        public string OrderId { get; private set; }

        public CompletePaymentCommand(string paymentId, string orderId)
        {
            PaymentId = paymentId;
            OrderId = orderId;
        }
    }
}
