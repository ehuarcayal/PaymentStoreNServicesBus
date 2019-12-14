using System;
using System.Collections.Generic;
using System.Text;
using UpgFisi.Common.Domain;

namespace Transactions.Domain
{
    public class Payment
    {
        public virtual string TransferId { get; protected set; }
        public virtual string CreditCardtId { get; protected set; }
        public virtual string OrderId { get; protected set; }
        public virtual Money Amount { get; protected set; }
        public virtual TransferState TransferState { get; protected set; }
        public virtual DateTime StartedAtUtc { get; protected set; }
        public virtual DateTime UpdatedAtUtc { get; protected set; }


        protected Payment()
        {
        }

        public Payment(
            string transferId,
            string creditCardtId,    
            string orderId,
            Money amount,
            TransferState transferState,            
            DateTime startedAtUtc
            )
        {
            TransferId = transferId;
            CreditCardtId = creditCardtId;
            OrderId = orderId;
            Amount = amount;
            TransferState = transferState;
            StartedAtUtc = startedAtUtc;
            UpdatedAtUtc = startedAtUtc;

        }

        public virtual void Complete()
        {
            TransferState = TransferState.COMPLETED;
        }

        public virtual void Reject()
        {
            TransferState = TransferState.REJECTED;
        }

        public virtual void ChangeUpdateAtUtc()
        {
            UpdatedAtUtc = DateTime.UtcNow;
        }
    }
}
