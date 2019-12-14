using System;
using UpgFisi.Common.Domain;

namespace CreditCards.Domain
{
    public class CustomerId : Identity
    {
        public CustomerId(String id)
        {
            new Identity(id);
        }

        public CustomerId()
        {

        }
    }
}