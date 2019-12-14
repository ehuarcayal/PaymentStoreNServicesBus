using System.Threading.Tasks;
using Transactions.Application.Dtos;

namespace Transactions.Application
{
    public interface ITransactionApplicationService
    {
        Task<PerformMoneyTransferResponseDto> PerformTransfer(PerformMoneyTransferRequestDto dto);
        Task<PerformMoneyTransferResponseDto> PerformPayment(PerformPaymentRequestDto dto);
    }
}