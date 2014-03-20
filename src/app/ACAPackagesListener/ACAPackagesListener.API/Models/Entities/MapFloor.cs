using System;

namespace ACAPackagesListener.API.Models.Entities
{
    [Serializable]
    public class MapFloor
    {
        public virtual Int32 Id { get; set; }
        public virtual Int32 MapId { get; set; }
        public virtual Int32 FloorNo { get; set; }
        public virtual bool Activo { get; set; }
    }
}
