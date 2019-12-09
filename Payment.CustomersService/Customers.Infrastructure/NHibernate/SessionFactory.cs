using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UpgFisi.Common.Infrastructure.NHibernate;

namespace Customers.Infrastructure.NHibernate
{
    public class SessionFactory
    {
        private readonly ISessionFactory _sessionFactory;

        public SessionFactory(string connectionString)
        {
            _sessionFactory = BuildSessionFactory(connectionString);
        }

        private static ISessionFactory BuildSessionFactory(string connectionString)
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(Assembly.GetExecutingAssembly())
                    .Conventions.Add(
                        ForeignKey.EndsWith("_id"),
                        ConventionBuilder.Property.When(criteria => criteria.Expect(x => x.Nullable, Is.Not.Set), x => x.Not.Nullable()))
                    .Conventions.Add<OtherConversions>()
                    .Conventions.Add<TableNameConvention>()
                );

            return configuration.BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}
