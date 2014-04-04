using System;

namespace ACAPackagesListener.API.Models.Entities
{
    public class Map
    {
        public virtual Int32 Id { get; set; }
        public virtual String TilesSource { get; set; }
        public virtual Int32 FloorsCount { get; set; }
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }
        public virtual bool Selected { get; set; }
    }
}
