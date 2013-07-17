using System;

namespace ACAPackagesListener.API.Models.Entities
{
    [Serializable]
    public class Device
    {
        public virtual Int32 DeviceId { get; set; }
        public virtual string Ip { get; set; }
        public virtual string LatLng { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Activo { get; set; }
        public virtual DateTime Created { get; set; }
    }
}
