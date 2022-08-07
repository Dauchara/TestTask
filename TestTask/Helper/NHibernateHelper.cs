using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using TestTask.Models;

namespace TestTask.Helper
{
    public class NHibernateHelper
    {

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();

        }


        private static ISessionFactory _sessionFactory;
        const string ConnectionString = @"data source=(local);initial catalog=TestHibernate;integrated security=True;";
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    CreateSessionFactory();

                return _sessionFactory;
            }
        }

        private static void CreateSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(ConnectionString).ShowSql)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CarModel>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false))
                .BuildSessionFactory();
        }
    }
}