using CreditCards.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCards.Application.Contracts
{
    public interface ICreditCardQueries
    {
        List<CreditCardDto> GetListBycustomer(String customerId);        
    }
}
