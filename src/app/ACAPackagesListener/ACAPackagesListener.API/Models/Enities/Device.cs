using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACAPackagesListener.API.Models.Enities
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
