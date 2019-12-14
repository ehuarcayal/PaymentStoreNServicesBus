using NServiceBus;
using System;
using System.Threading.Tasks;
using Transactions.Application.Dtos;
using Transactions.Messages.Commands;

namespace Transactions.Application
{
    public class TransactionApplicationService : ITransactionApplicationService
    {
        private readonly IMessageSession _messageSession;

        public  TransactionApplicationService(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        public async Task<PerformMoneyTransferResponseDto> PerformPayment(PerformPaymentRequestDto dto)
        {
            try
            {
                var paymentId = Guid.NewGuid().ToString();
                var command = new RequestPaymentCommand(
                        paymentId,
                        dto.creditCardId,
                        dto.orderId,
                        dto.Amount
                    );
                await _messageSession.Send(command).ConfigureAwait(false);
                return new PerformMoneyTransferResponseDto
                {
                    Response = "OK"
                };
            }
            catch (Exception ex)
            {
                return new PerformMoneyTransferResponseDto
                {
                    Response = "ERROR: " + ex.Message + " -- " + ex.StackTrace
                };
            }
        }

        public async Task<PerformMoneyTransferResponseDto> PerformTransfer(PerformMoneyTransferRequestDto dto)
        {
            try
            {
                var transferId = Guid.NewGuid().ToString();
                var command = new RequestMoneyTransferCommand(
                    transferId,
                    dto.SourceAccountId, 
                    dto.DestinationAccountId,
                    dto.Amount, 
                    dto.Description
                );
                await _messageSession.Send(command).ConfigureAwait(false);
                return new PerformMoneyTransferResponseDto
                {
                    Response = "OK"
                };
            }
            catch(Exception ex)
            {
                return new PerformMoneyTransferResponseDto
                {
                    Response = "ERROR: " + ex.Message + " -- " + ex.StackTrace
                };
            }
        }
    }
}