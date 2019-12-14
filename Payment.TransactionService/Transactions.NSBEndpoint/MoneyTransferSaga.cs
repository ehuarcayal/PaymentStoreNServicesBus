using System.Threading.Tasks;
using Accounts.Messages.Commands;
using Accounts.Messages.Events;
using CreditCards.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Transactions.Messages.Commands;
using Transactions.Messages.Events;

namespace Transactions.NSBEndpoint
{
    public class MoneyTransferSaga :
        Saga<MoneyTransferSagaData>,
        IAmStartedByMessages<MoneyTransferRequestedEvent>,
        IAmStartedByMessages<PaymentRequestedEvent>,
        IHandleMessages<Accounts.Messages.Events.MoneyWithdrawnEvent>,
        IHandleMessages<Accounts.Messages.Events.WithdrawMoneyRejectedEvent>,
        IHandleMessages<MoneyDepositedEvent>,
        IHandleMessages<DepositMoneyRejectedEvent>,
        IHandleMessages<SourceAccountNotFoundEvent>,
        IHandleMessages<DestinationAccountNotFoundEvent>,        
        IHandleMessages<CreditCards.Messages.Events.MoneyWithdrawnEvent>,
        IHandleMessages<SourceCreditCardNotFountEvent>,
        IHandleMessages<CreditCards.Messages.Events.WithdrawMoneyRejectedEvent>
    {
        static readonly ILog log = LogManager.GetLogger<PerformMoneyTransferHandler>();

        public async Task Handle(MoneyTransferRequestedEvent message, IMessageHandlerContext context)
        {
            log.Info($"MoneyTransferRequestedEvent, TransferId = {message.TransferId}");
            Data.TransferId = message.TransferId;
            Data.SourceAccountId = message.SourceAccountId;
            Data.DestinationAccountId = message.DestinationAccountId;
            Data.Amount = message.Amount;
            var command = new WithdrawMoneyCommand(
                Data.SourceAccountId, 
                Data.TransferId,
                Data.Amount
            );
            await context.Send(command).ConfigureAwait(false);
        }

        public async Task Handle(PaymentRequestedEvent message, IMessageHandlerContext context)
        {
            Data.TransferId = message.TransactionId;
            Data.SourceAccountId = message.CreditCardId;
            Data.Amount = message.Amount;
            var command = new CreditCards.Messages.Commands.WithdrawMoneyCommand(
                Data.SourceAccountId,
                Data.TransferId,
                Data.Amount,
                message.OrderId
            );
            await context.Send(command).ConfigureAwait(false);
        }

        public async Task Handle(SourceAccountNotFoundEvent message, IMessageHandlerContext context)
        {
            log.Info($"SourceAccountNotFoundEvent, TransactionId = {message.TransactionId}");
            var command = new RejectMoneyTransferCommand(
                Data.TransferId
            );
            await context.SendLocal(command).ConfigureAwait(false);
            MarkAsComplete();
        }

        public async Task Handle(Accounts.Messages.Events.MoneyWithdrawnEvent message, IMessageHandlerContext context)
        {
            log.Info($"MoneyWithdrawnEvent, TransactionId = {message.TransactionId}");
            var command = new DepositMoneyCommand(
                Data.DestinationAccountId,
                Data.TransferId,
                Data.Amount
            );
            await context.Send(command).ConfigureAwait(false);
        }

        public async Task Handle(Accounts.Messages.Events.WithdrawMoneyRejectedEvent message, IMessageHandlerContext context)
        {
            log.Info($"WithdrawMoneyRejectedEvent, TransactionId = {message.TransactionId}");
            var command = new RejectMoneyTransferCommand(
                Data.TransferId
            );
            await context.Send(command).ConfigureAwait(false);
        }

        public async Task Handle(DestinationAccountNotFoundEvent message, IMessageHandlerContext context)
        {
            log.Info($"DestinationAccountNotFoundEvent, TransactionId = {message.TransactionId}");
            var returnMoneyCommand = new ReturnMoneyCommand(
                Data.SourceAccountId,
                Data.Amount
            );
            await context.Send(returnMoneyCommand).ConfigureAwait(false);
            var rejectMoneyTransferCommand = new RejectMoneyTransferCommand(
                Data.TransferId
            );
            await context.SendLocal(rejectMoneyTransferCommand).ConfigureAwait(false);
            MarkAsComplete();
        }

        public async Task Handle(MoneyDepositedEvent message, IMessageHandlerContext context)
        {
            log.Info($"MoneyDepositedEvent, TransactionId = {message.TransactionId}");
            var command = new CompleteMoneyTransferCommand(
                Data.TransferId
            );
            await context.SendLocal(command).ConfigureAwait(false);
            MarkAsComplete();
        }

        public async Task Handle(CreditCards.Messages.Events.MoneyWithdrawnEvent message, IMessageHandlerContext context)
        {
            var command = new CompletePaymentCommand(
                message.TransferId,
                message.OrderId
            );
            await context.SendLocal(command).ConfigureAwait(false);
            MarkAsComplete();
        }

        public async Task Handle(DepositMoneyRejectedEvent message, IMessageHandlerContext context)
        {
            log.Info($"DepositMoneyRejectedEvent, TransferId = {message.TransactionId}");
            var command = new RejectMoneyTransferCommand(
                Data.TransferId
            );
            await context.SendLocal(command).ConfigureAwait(false);
            MarkAsComplete();
        }

        public async Task Handle(SourceCreditCardNotFountEvent message, IMessageHandlerContext context)
        {
            var command = new RejectCreditCardCommand(
               message.TransactionId,
               message.OrderId
           );
            await context.SendLocal(command).ConfigureAwait(false);
            MarkAsComplete();
        }

        public async Task Handle(CreditCards.Messages.Events.WithdrawMoneyRejectedEvent message, IMessageHandlerContext context)
        {
            var command = new RejectCreditCardCommand(
               message.TransactionId,
               message.OrderId
           );
            await context.Send(command).ConfigureAwait(false);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MoneyTransferSagaData> mapper)
        {
            mapper.ConfigureMapping<MoneyTransferRequestedEvent>(message => message.TransferId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<Accounts.Messages.Events.MoneyWithdrawnEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<Accounts.Messages.Events.WithdrawMoneyRejectedEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<MoneyDepositedEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<DepositMoneyRejectedEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<SourceAccountNotFoundEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<DestinationAccountNotFoundEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<PaymentRequestedEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<CreditCards.Messages.Events.MoneyWithdrawnEvent> (message => message.TransferId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<SourceCreditCardNotFountEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<CreditCards.Messages.Events.WithdrawMoneyRejectedEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);
        }
    }
}