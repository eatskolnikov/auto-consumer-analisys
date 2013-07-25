using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Persistance;
using NHibernate.Criterion;

namespace ACAPackagesListener.API.Models.Repositories
{
    public class NHUserRepository :  NHWritableRepository<User>, IUserRepository
    {
        public User Authenticate(string username, string password)
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            {
                return
                    session.CreateCriteria<User>()
                        .Add(Restrictions.Eq("Username", username))
                        .Add(Restrictions.Eq("Password", password))
                        .UniqueResult<User>();
            }
        }
    }
}
