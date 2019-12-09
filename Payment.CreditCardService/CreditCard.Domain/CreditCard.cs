using System;
using UpgFisi.Common.Domain;

namespace CreditCards.Domain
{
    public class CreditCard
    {
        public virtual string creditCardId { get; protected set; }
        public virtual CustomerId customerId { get; protected set; }
        public virtual string type { get; protected set; }
        public virtual string number { get; protected set; }
        public virtual DateTime expirationDate { get; protected set; }
        public virtual Money amountLimit { get; protected set; }
        public virtual string ccv { get; protected set; }
    }
}
