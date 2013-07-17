using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACAPackagesListener.API.Models.Entities
{
    public class Package
    {
        public virtual Int32 PackageId { get; set; }
        public virtual Device Device { get; set; }
        public virtual String Ip { get; set; }
        public virtual String Message { get; set; }
        public virtual Boolean Parsed { get; set; }
    }
}
