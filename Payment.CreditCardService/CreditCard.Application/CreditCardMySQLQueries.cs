using CreditCards.Application.Contracts;
using CreditCards.Application.Dto;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreditCards.Application
{
    public class CreditCardMySQLQueries : ICreditCardQueries
    {
        public List<CreditCardDto> GetListBycustomer(string customerId)
        {
            string sql = @"
                    SELECT 
                        c.credit_card_id AS id,
                        c.number_card AS numberCard,
                        c.customer_id AS customerId,
                        c.expiration_at_utc AS expirationDate                        
                    FROM 
                        credit_card c
                    WHERE c.customer_id = @customerId";
            string connectionString = Environment.GetEnvironmentVariable("MYSQL_STRCON_CORE_CREDITCARDS");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    List<CreditCardDto> creditCards = connection
                        .Query<CreditCardDto>(sql, new
                        {
                            customerId = customerId                            
                        })
                        .ToList();
                    return creditCards;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return new List<CreditCardDto>();
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
