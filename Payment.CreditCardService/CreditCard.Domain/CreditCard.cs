using System;
using UpgFisi.Common.Domain;

namespace CreditCards.Domain
{
    public class CreditCard
    {
        public virtual string creditCardId { get; set; }
        public virtual CustomerId customerId { get; set; }
        public virtual string type { get; set; }
        public virtual string number { get; set; }
        public virtual DateTime expirationDate { get; set; }
        public virtual Money amountLimit { get; set; }
        public virtual string ccv { get; set; }

        public CreditCard(
            string _creditCardId,
            string _customerId,
            String _type,
            String _number,
            DateTime _expirationDate,
            Decimal _mount,
            String _ccv            
            )
        {
            customerId = new CustomerId(_customerId);            
            creditCardId = _creditCardId;
            type = _type;
            expirationDate = _expirationDate;
            type = _type;
            number = _number;
            ccv = _ccv;
            amountLimit = new Money(_mount, Currency.PEN); 
        }

        public CreditCard()
        {
        }

        public virtual void WithdrawMoney(decimal amount)
        {
            if (CanWithdrawMoney(amount))
            {
                var money = new Money(amount, Currency.USD);
                amountLimit = amountLimit.Subtract(money);
            }
        }

        public virtual bool CanWithdrawMoney(decimal amount)
        {
            return amountLimit.Amount >= amount;
        }
      
    }
}
