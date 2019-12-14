using CreditCards.Domain;
using CreditCards.Messages.Commands;
using CreditCards.Messages.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditCards.NSBEndpoint
{
    class WithdrawMoneyHandler : IHandleMessages<WithdrawMoneyCommand>
    {
        public async Task Handle(WithdrawMoneyCommand message, IMessageHandlerContext context)
        {
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var creditCardAggregate = nhibernateSession.Get<CreditCard>(message.CreditCardId);
            if (creditCardAggregate == null)
            {
                var sourceAccountNotFoundEvent = new SourceCreditCardNotFountEvent(message.PaymentId, message.OrderId);
                await context.Publish(sourceAccountNotFoundEvent);
            }
            else
            {
                if (creditCardAggregate.CanWithdrawMoney(message.Amount))
                {
                    creditCardAggregate.WithdrawMoney(message.Amount);                    
                    nhibernateSession.Save(creditCardAggregate);
                    var moneyWithdrawnEvent = new MoneyWithdrawnEvent
                    (
                        message.CreditCardId,
                        message.PaymentId,
                        message.OrderId,
                        message.Amount,
                        creditCardAggregate.amountLimit.Amount
                    );
                    await context.Publish(moneyWithdrawnEvent);
                }
                else
                {
                    var withdrawMoneyRejectedEvent = new WithdrawMoneyRejectedEvent
                    (
                        message.PaymentId,
                        message.OrderId
                    );
                    await context.Publish(withdrawMoneyRejectedEvent);
                }
            }
        }
    }
}
