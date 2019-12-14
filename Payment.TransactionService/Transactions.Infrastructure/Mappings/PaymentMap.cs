using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using Transactions.Domain;

namespace Transactions.Infrastructure.Mappings
{
    public class PaymentMap : ClassMap<Payment>
    {
        public PaymentMap()
        {
            Id(x => x.TransferId).Column("transfer_id");
            Map(x => x.CreditCardtId).Column("credit_card_id");
            Map(x => x.OrderId).Column("order_id");
            Component(x => x.Amount, m =>
            {
                m.Map(x => x.Amount, "amount");
            });
            Map(x => x.TransferState).CustomType<int>().Column("transfer_state_id");
            Map(x => x.StartedAtUtc).Column("started_at_utc");
            Map(x => x.UpdatedAtUtc).Column("updated_at_utc");
        }
    }
}
