using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCards.Messages.Events
{
    public class MoneyWithdrawnEvent : IEvent
    {
        public string CreditCardId { get; protected set; }
        public string OrderId { get; protected set; }
        public string TransferId { get; protected set; }
        public decimal Amount { get; protected set; }
        public decimal Balance { get; protected set; }

        public MoneyWithdrawnEvent(string creditCardId, string transferId, string orderId, decimal amount, decimal balance)
        {
            CreditCardId = creditCardId;
            OrderId = orderId;
            TransferId = transferId;
            Amount = amount;
            Balance = balance;
        }
    }
}
