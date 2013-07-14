using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACAPackagesListener.API.Models.Enities
{
    [Serializable]
    public class User
    {
        public virtual Int32 UserId { get; set; }
        public virtual String Username { get; set; }
        public virtual String Password { get; set; }
        public virtual bool IsAdmin { get; set; }
    }
}
