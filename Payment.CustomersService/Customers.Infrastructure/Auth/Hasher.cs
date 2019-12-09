using Customers.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Infrastructure.Auth
{
    public class Hasher
    {
        public string HashPassword(string password)
        {
            PasswordHasher<Customer> passwordHasher = new PasswordHasher<Customer>();
            return passwordHasher.HashPassword(null, password);
        }

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            PasswordHasher<Customer> passwordHasher = new PasswordHasher<Customer>();
            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);            
            return result == PasswordVerificationResult.Success;
        }
    }
}
