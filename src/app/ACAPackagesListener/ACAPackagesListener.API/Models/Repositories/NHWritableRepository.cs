using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACAPackagesListener.API.Persistance;

namespace ACAPackagesListener.API.Models.Repositories
{
    public abstract class NHWritableRepository<T>
           : NHReadOnlyRepository<T>, IWriteableCommonRepository<T> where T : class
    {
        public virtual void Add(T element)
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(element);
                transaction.Commit();
            }
        }

        public virtual void Update(T element)
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Update(element);
                transaction.Commit();
            }
        }

        public virtual void Remove(T element)
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(element);
                transaction.Commit();
            }
        }
    }
}
