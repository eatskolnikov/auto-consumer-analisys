
using System.Linq;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Persistance;

namespace ACAPackagesListener.API.Models.Repositories
{

    public class NHMapRepository : NHWritableRepository<Map>, IMapRepository
    {
        public Map GetSelected()
        {
            var all = GetAll();
            return all.FirstOrDefault(x => x.Selected);
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
