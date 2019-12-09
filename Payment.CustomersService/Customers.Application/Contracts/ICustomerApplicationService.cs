using Customers.Application.Dto;

namespace Customers.Application
{
    public interface ICustomerApplicationService
    {
        ResponseDto Register(NewCustomerDto newAccountDto);
    }
}