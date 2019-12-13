using Customers.Application.Contracts;
using Customers.Application.Dto;
using MySql.Data.MySqlClient;
using System;
using Dapper;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Customers.Domain;

namespace Customers.Application.Queries
{
    public class CustomerMySQLDapperQueries : ICustomerQueries
    {
        public CustomerDto getCustomer(string customerId)
        {
            string sql = @"
                    SELECT 
                        c.customer_id AS id,
                        c.first_name AS firstName,
                        c.last_name AS lastName,
                        c.identity_document AS identityDocument,
                        c.active
                    FROM 
                        customer c
                    WHERE c.customer_id = @customerId ";
            string connectionString = Environment.GetEnvironmentVariable("MYSQL_STRCON_CORE_CUSTOMERS");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    CustomerDto customers = connection
                        .Query<CustomerDto>(sql, new
                        {
                            customerId = customerId
                        })
                        .First();
                    return customers;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return new CustomerDto();
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

        public Customer getCustomerbyUserName(string userName)
        {
            string sql = @"
                    SELECT 
                        c.customer_id AS CustomerId,
                        c.user_name AS user,
                        c.password_hash AS passwordHash
                    FROM 
                        customer c
                    WHERE c.user_name = @userName ";
            string connectionString = Environment.GetEnvironmentVariable("MYSQL_STRCON_CORE_CUSTOMERS");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Customer customers = connection
                        .Query<Customer>(sql, new
                        {
                            userName = userName
                        })
                        .First();
                    return customers;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return null;
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

        public List<CustomerDto> GetListPaginated(int page = 0, int pageSize = 5)
        {
            string sql = @"
                    SELECT 
                        c.customer_id AS id,
                        c.first_name AS firstName,
                        c.last_name AS lastName,
                        c.identity_document AS identityDocument,
                        c.active
                    FROM 
                        customer c
                        JOIN (SELECT c2.customer_id FROM customer c2 ORDER BY c2.last_name ASC, c2.first_name ASC LIMIT @Page, @PageSize)
                            AS c3 ON c.customer_id = c3.customer_id
                    ORDER BY 
                        c.last_name ASC, c.first_name ASC;";
            string connectionString = Environment.GetEnvironmentVariable("MYSQL_STRCON_CORE_CUSTOMERS");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    List<CustomerDto> customers = connection
                        .Query<CustomerDto>(sql, new
                        {
                            Page = page,
                            PageSize = pageSize
                        })
                        .ToList();
                    return customers;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return new List<CustomerDto>();
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
