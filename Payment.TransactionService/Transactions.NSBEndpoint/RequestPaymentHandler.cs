using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transactions.Domain;
using Transactions.Messages.Commands;
using Transactions.Messages.Events;
using UpgFisi.Common.Domain;

namespace Transactions.NSBEndpoint
{
    public class RequestPaymentHandler : IHandleMessages<RequestPaymentCommand>
    {
        public async Task Handle(RequestPaymentCommand message, IMessageHandlerContext context)
        {
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var paymentAggregate = new Payment(
                message.TransactionId,
                message.CreditCardId,
                message.OrderId,
                Money.Dollars(message.Amount),
                TransferState.STARTED,                
                DateTime.UtcNow
            );
            nhibernateSession.Save(paymentAggregate);
            var paymentRequestedEvent = new PaymentRequestedEvent
            (
                message.TransactionId,
                message.CreditCardId,                 
                message.Amount,
                message.OrderId
            );
            await context.Publish(paymentRequestedEvent);
        }
    }
}
