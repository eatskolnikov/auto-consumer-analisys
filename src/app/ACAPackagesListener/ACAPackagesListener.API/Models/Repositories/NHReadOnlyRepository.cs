using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using ACAPackagesListener.API.Persistance;

namespace ACAPackagesListener.API.Models.Repositories
{
    public class NHReadOnlyRepository<T> : IReadonlyCommonRepository<T> where T : class
    {

        public virtual IEnumerable<T> GetAll()
        {
            using(var session = NHibernateHelper.GetCurrentSession())
            {
                var q = session.QueryOver<T>();
                return q.List<T>();
            }
        }

        public virtual T GetById<TKey>(TKey id)
        {
            using(var session = NHibernateHelper.GetCurrentSession())
            {
                return session.Get<T>(id);
            }
        }
    }
}