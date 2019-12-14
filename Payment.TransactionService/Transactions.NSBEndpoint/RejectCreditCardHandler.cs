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
    public class RejectCreditCardHandler : IHandleMessages<RejectCreditCardCommand>
    {
        public async Task Handle(RejectCreditCardCommand message, IMessageHandlerContext context)
        {
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var transferAggregate = nhibernateSession.Get<Payment>(message.TransferId);
            transferAggregate.Reject();
            transferAggregate.ChangeUpdateAtUtc();
            nhibernateSession.Save(transferAggregate);
            var moneyTransferRejectedEvent = new PaymentRejectedEvent
            (
                message.TransferId,
                message.OrderId
            );
            await context.Publish(moneyTransferRejectedEvent);
        }
    }
}
