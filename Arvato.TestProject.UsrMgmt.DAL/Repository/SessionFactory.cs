using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Data.SqlClient;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using System.Reflection;

namespace Arvato.TestProject.UsrMgmt.DAL.Repository
{
   public class SessionFactory
    {

        string connString = "Data Source=.;Initial Catalog=UserManagement;Integrated Security=True";
        //public ISessionFactory CreateSessionFactory()
        //{
        //    try
        //    {
        //        return Fluently.Configure()
        //    .Database(MsSqlConfiguration
        //                  .MsSql2008
        //                 .ConnectionString(connString))
        //    .Mappings(m => m.FluentMappings
        //                       .AddFromAssemblyOf<UserMapping>())
        //    .BuildSessionFactory();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }



        //}
        public ISessionFactory CreateSessionFactory()
        {
            // configure nhibernate
            Configuration config = MsSqlConfiguration.MsSql2008
                .ConnectionString(connString)
                .UseReflectionOptimizer()
                .ShowSql()
                .ConfigureProperties(new Configuration());

            // load mappings from this assembly
            config.AddMappingsFromAssembly(Assembly.GetExecutingAssembly());

            // build factory
            ISessionFactory sessionfactory = config.BuildSessionFactory();

            return sessionfactory;
        }

    }
}
