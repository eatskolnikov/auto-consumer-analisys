using ACAPackagesListener.API.Models.Entities;

namespace ACAPackagesListener.API.Models.Repositories
{
    public interface IMapRepository : IWriteableCommonRepository<MallMap>
    {
        MallMap GetSelected();
        void ChangeSelected(int mapId);
    }
}
