using Customers.Application.Dto;
using Customers.Domain;
using Customers.Infrastructure.NHibernate;
using Microsoft.AspNetCore.Http;
using System;
using UpgFisi.Common.Domain;

namespace Customers.Application
{
    public class CustomerApplicationService : ICustomerApplicationService
    {        
        private readonly SessionFactory _sessionFactory;       

        public CustomerApplicationService(SessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;            
        }

        public ResponseDto Register(NewCustomerDto newAccountDto)
        {
            try
            {                
                var customer = new Customer(
                    newAccountDto.CustomerId,
                    newAccountDto.firstName,
                    newAccountDto.lastName,
                    newAccountDto.identityDocument,
                    true,
                    DateTime.UtcNow
                );

                var nhibernateSession = _sessionFactory.OpenSession();                
                nhibernateSession.Save(customer);
                nhibernateSession.Flush();
                return new ResponseDto
                {
                    HttpStatusCode = StatusCodes.Status201Created,
                    Response = new ApiStringResponse("Customer Created")
                };                
            }
            catch (Exception ex)
            {
                //TODO: Log exception async, for now write exception in the console
                Console.WriteLine(ex.Message);
                return new ResponseDto
                {
                    HttpStatusCode = StatusCodes.Status500InternalServerError,
                    Response = new ApiStringResponse("Server Internal Error")
                };
            }
        }


    }
}