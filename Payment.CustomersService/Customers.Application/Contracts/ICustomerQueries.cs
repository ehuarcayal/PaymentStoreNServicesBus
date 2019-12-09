using System;
using System.Collections.Generic;
using System.Text;
using Customers.Application.Dto;
using Customers.Domain;

namespace Customers.Application.Contracts
{
    public interface ICustomerQueries
    {
        List<CustomerDto> GetListPaginated(int page = 0, int pageSize = 5);
        CustomerDto getCustomer(String customerId);
        Customer getCustomerbyUserName(String userName);

    }
}
