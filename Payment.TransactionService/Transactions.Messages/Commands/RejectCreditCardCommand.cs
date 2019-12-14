using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transactions.Messages.Commands
{
    public class RejectCreditCardCommand : ICommand
    {
        public string TransferId { get; private set; }
        public string OrderId { get; private set; }

        public RejectCreditCardCommand(string transferId, string orderId)
        {
            TransferId = transferId;
            OrderId = orderId;
        }
    }
}
