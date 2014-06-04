using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate;
using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Reflection;

namespace Arvato.TestProject.UsrMgmt.DAL.Repository
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static string connectionString;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                return _sessionFactory;

            }
        }

        private static void InitializeSessionFactory()
        {
            // configure nhibernate
            Configuration config = MsSqlConfiguration.MsSql2012
                .ConnectionString(connectionString)
                .UseReflectionOptimizer()
                //.ShowSql()
                .ConfigureProperties(new Configuration());

            // load mappings from this assembly
            config.AddMappingsFromAssembly(Assembly.GetExecutingAssembly());

            // build factory
            _sessionFactory = config.BuildSessionFactory();

        }

        public static ISession OpenSession(string connString)
        {
            connectionString = connString;
            return SessionFactory.OpenSession();
        }
    }
}
