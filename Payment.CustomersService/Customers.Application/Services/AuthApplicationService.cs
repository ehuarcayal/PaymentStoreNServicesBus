using Customers.Application.Contracts;
using Customers.Application.Dto;
using Customers.Domain;
using Customers.Infrastructure.Auth;
using Customers.Infrastructure.NHibernate;
using Microsoft.AspNetCore.Http;
using System;

namespace Customers.Application.Services
{
    public class AuthApplicationService : IAuthApplicationService
    {
        private readonly Hasher _hasher;
        private readonly SessionFactory _sessionFactory;
        private readonly ICustomerQueries _customerQueries;

        public AuthApplicationService(SessionFactory sessionFactory, Hasher hasher, ICustomerQueries customerQueries)
        {
            _hasher = hasher;
            _sessionFactory = sessionFactory;
            _customerQueries = customerQueries;
        }

        public LoginResponseDto Login(LoginDto loginDto)
        {
            try
            {
                //var nhibernateSession = _sessionFactory.OpenSession();
                var customer = _customerQueries.getCustomerbyUserName(loginDto.Name);                
                if (customer == null)
                {
                    return new LoginResponseDto
                    {
                        HttpStatusCode = StatusCodes.Status401Unauthorized,
                        Response = new ApiStringResponse("Login Invalido")
                    };
                }
                if (!_hasher.VerifyHashedPassword(customer.passwordHash, loginDto.Password))
                {
                    return new LoginResponseDto
                    {
                        HttpStatusCode = StatusCodes.Status401Unauthorized,
                        Response = new ApiStringResponse("Login Invalido")
                    };
                }

                CustomerLoginResponseDto customerData = new CustomerLoginResponseDto();
                customerData.customerId = customer.CustomerId;

                return new LoginResponseDto
                {
                    HttpStatusCode = StatusCodes.Status200OK,
                    Response = new ApiStringResponse("Login Correcto"),
                    customerData = customerData
                };
            }
            catch (Exception ex)
            {                
                //TODO: Log exception async, for now write exception in the console
                Console.WriteLine(ex.Message);
                return new LoginResponseDto
                {
                    HttpStatusCode = StatusCodes.Status500InternalServerError,
                    Response = new ApiStringResponse("Error interno en el Servidor")
                };
            }
        }
    }   
}
