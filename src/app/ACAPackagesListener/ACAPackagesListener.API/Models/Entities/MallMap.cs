using System;

namespace ACAPackagesListener.API.Models.Entities
{
    [Serializable]
    public class MallMap
    {
        public virtual Int32 MapId { get; set; }
        public virtual String TilesSource { get; set; }
        public virtual Int32 FloorsCount { get; set; }
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }
        public virtual bool Selected { get; set; }
    }
}
