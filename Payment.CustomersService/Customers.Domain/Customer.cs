using System;

namespace Customers.Domain
{
    public class Customer
    {
        public virtual string CustomerId { get; set; }
        public virtual string user { get; set; }
        public virtual string passwordHash { get; set; }
        public virtual FirstName FirstName { get; set; }
        public virtual LastName LastName { get; set; }
        public virtual IdentityDocument IdentityDocument { get; set; }
        public virtual bool Active { get; set; }
        public virtual DateTime CreatedAtUtc { get; set; }
        public virtual DateTime UpdatedAtUtc { get; set; }

        public Customer(
            string customerId,
            String firstName,
            String lastName,
            String identityDocument,
            bool active,
            DateTime createdAtUtc            
            )
        {
            CustomerId = customerId;
            FirstName = new FirstName(firstName);
            LastName = new LastName(lastName);
            IdentityDocument = new IdentityDocument(identityDocument);
            Active = active;
            CreatedAtUtc = createdAtUtc;
            UpdatedAtUtc = createdAtUtc;
        }

        public Customer()
        {
        }
    }
}