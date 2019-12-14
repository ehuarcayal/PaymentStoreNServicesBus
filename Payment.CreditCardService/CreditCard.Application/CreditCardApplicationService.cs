
using CreditCards.Application.Contracts;
using CreditCards.Application.Dto;
using CreditCards.Domain;
using CreditCards.Infrastructure.NHibernate;
using Microsoft.AspNetCore.Http;
using System;
using UpgFisi.Common.Domain;

namespace CreditCards.Application
{
    public class CreditCardApplicationService : ICreditCardApplicationService
    {
        private readonly SessionFactory _sessionFactory;

        public CreditCardApplicationService(SessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public ResponseDto Register(NewCreditCardDto newCreditCardDto)
        {
            try
            {
                var creditCard = new CreditCard(
                    newCreditCardDto.creditCardId,
                    newCreditCardDto.CustomerId,
                    newCreditCardDto.type,
                    newCreditCardDto.numberCard,
                    newCreditCardDto.expiration,
                    newCreditCardDto.mount,
                    newCreditCardDto.ccv
                );

                var nhibernateSession = _sessionFactory.OpenSession();
                nhibernateSession.Save(creditCard);
                nhibernateSession.Flush();
                return new ResponseDto
                {
                    HttpStatusCode = StatusCodes.Status201Created,
                    Response = new ApiStringResponse("CreditCard Created")
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
