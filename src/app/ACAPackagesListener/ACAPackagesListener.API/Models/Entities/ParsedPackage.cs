using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACAPackagesListener.API.Models.Entities
{
    [Serializable]
    public class ParsedPackage
    {
        public virtual Int32 ParsedPackageId { get; set; }
        //public virtual Package Package { get; set; }
        public virtual Int32 PackageDate { get; set; }
        public virtual Int32 PackageTimeOfDay { get; set; }
        public virtual String LatLng { get; set; }
        public virtual String MAC { get; set; }
        public virtual Boolean Activo { get; set; }
        public virtual DateTime Created { get; set; }
    }
}
