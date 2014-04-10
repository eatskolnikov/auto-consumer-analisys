
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Persistance;
using NHibernate.Mapping;

namespace ACAPackagesListener.API.Models.Repositories
{

    public class NHMapRepository : NHWritableRepository<MallMap>, IMapRepository
    {
        public MallMap GetSelected()
        {
            var all = GetAll();
            return all.FirstOrDefault(x => x.Selected);
        }
        public override void Add(MallMap element)
        {
            if (element.Selected)
            {
                using (var session = NHibernateHelper.GetCurrentSession())
                using (var transaction = session.BeginTransaction())
                {
                    var query = "UPDATE Maps SET Selected=0";
                    session.CreateSQLQuery(query).ExecuteUpdate();
                    transaction.Commit();
                }
            }
            base.Add(element);
        }
        public void ChangeSelected(int mapId)
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            using (var transaction = session.BeginTransaction())
            {
                var query = "UPDATE Maps SET Selected=0";
                session.CreateQuery(query).ExecuteUpdate();
                query = "UPDATE Maps SET Selected=1 WHERE Id=" + mapId;
                session.CreateQuery(query).ExecuteUpdate(); 
                transaction.Commit();
            }
        }
    }
}
