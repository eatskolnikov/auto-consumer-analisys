using System.Reflection;
using NHibernate;
using NHibernate.Cfg;


namespace ACAPackagesListener.API.Persistance
{
    public sealed class NHibernateHelper
    {
        private static ISessionFactory SessionFactory { get; set; }

        private static void OpenSession()
        {
            var configuration = new Configuration();
            configuration.AddAssembly(Assembly.GetCallingAssembly());
            SessionFactory = configuration.BuildSessionFactory();
        }

        public static ISession GetCurrentSession()
        {
            if (SessionFactory == null)
                OpenSession();
            return SessionFactory.OpenSession();
        }

        public static void CloseSessionFactory()
        {
            if (SessionFactory != null)
                SessionFactory.Close();
        }
    }
}
