using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACAPackagesListener.API.Models.Entities;

namespace ACAPackagesListener.API.Models.Repositories
{
    public class NHMapFloorRepository : NHWritableRepository<MapFloor>, IMapFloorRepository
    {
        public IEnumerable<MapFloor> GetByMapId(int mapId)
        {
            return this.GetAll().Where(x => x.MapId == mapId);
        }
    }
}
