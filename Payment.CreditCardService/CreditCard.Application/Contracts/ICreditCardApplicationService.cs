
using CreditCards.Application.Dto;
using UpgFisi.Common.Domain;

namespace CreditCards.Application.Contracts
{
    public interface ICreditCardApplicationService
    {
        ResponseDto Register(NewCreditCardDto newCreditCardDto);
    }
}
