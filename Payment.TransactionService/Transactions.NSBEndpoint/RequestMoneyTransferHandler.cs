using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;
using Transactions.Domain;
using Transactions.Messages.Commands;
using Transactions.Messages.Events;
using UpgFisi.Common.Domain;

namespace Transactions.NSBEndpoint
{
    public class PerformMoneyTransferHandler : IHandleMessages<RequestMoneyTransferCommand>
    {
        static readonly ILog log = LogManager.GetLogger<PerformMoneyTransferHandler>();

        public async Task Handle(RequestMoneyTransferCommand message, IMessageHandlerContext context)
        {
            log.Info($"Received RequestMoneyTransferCommand, TransferId = {message.TransferId}");
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var transferAggregate = new Transfer(
                message.TransferId,
                new AccountId(message.SourceAccountId),
                new AccountId(message.DestinationAccountId),
                Money.Dollars(message.Amount),
                TransferState.STARTED,
                message.Description,
                DateTime.UtcNow
            );
            nhibernateSession.Save(transferAggregate);
            var moneyTransferRequestedEvent = new MoneyTransferRequestedEvent
            (
                message.TransferId,
                message.SourceAccountId,
                message.DestinationAccountId,
                message.Amount,
                message.Description
            );
            await context.Publish(moneyTransferRequestedEvent);
        }
    }
}