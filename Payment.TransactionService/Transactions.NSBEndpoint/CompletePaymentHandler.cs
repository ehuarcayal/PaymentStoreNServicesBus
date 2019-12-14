using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transactions.Domain;
using Transactions.Messages.Commands;
using Transactions.Messages.Events;

namespace Transactions.NSBEndpoint
{
    public class CompletePaymentHandler : IHandleMessages<CompletePaymentCommand>
    {     

        public async Task Handle(CompletePaymentCommand message, IMessageHandlerContext context)
        {
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var paymentAggregate = nhibernateSession.Get<Payment>(message.PaymentId);
            paymentAggregate.Complete();
            paymentAggregate.ChangeUpdateAtUtc();
            nhibernateSession.Save(paymentAggregate);
            var paymentCompleteEvent = new PaymentCompleteEvent
            (
                message.PaymentId,
                message.OrderId
            );
            await context.Publish(paymentCompleteEvent);
        }
    }
}
