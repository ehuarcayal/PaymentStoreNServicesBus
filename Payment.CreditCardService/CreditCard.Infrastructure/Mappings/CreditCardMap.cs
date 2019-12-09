using CreditCards.Domain;
using FluentNHibernate.Mapping;

namespace CreditCards.Infrastructure.Mappings
{
    public class CreditCardMap : ClassMap<CreditCard>
    {
        public CreditCardMap()
        {
            Id(x => x.creditCardId).Column("credit_card_id");
            Component(x => x.customerId, m =>
            {
                m.Map(x => x.Id, "customer_id");
            });
            Map(x => x.type).Column("type_id");
            Map(x => x.number).Column("number_card");
            Map(x => x.expirationDate).Column("expiration_at_utc");
            Component(x => x.amountLimit, m =>
            {
                m.Map(x => x.Amount, "amount_limit");
            });
            Map(x => x.ccv).Column("ccv");
        }
    }
}
