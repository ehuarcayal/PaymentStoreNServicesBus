using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCards.Messages.Commands
{
    public class WithdrawMoneyCommand : ICommand
    {
        public string CreditCardId { get; private set; }
        public string PaymentId { get; private set; }
        public string OrderId { get; private set; }
        public decimal Amount { get; private set; }

        public WithdrawMoneyCommand(string creditCardId, string paymentId, decimal amount, String orderId)
        {
            CreditCardId = creditCardId;
            PaymentId = paymentId;
            Amount = amount;
            OrderId = orderId;
        }
    }
}
