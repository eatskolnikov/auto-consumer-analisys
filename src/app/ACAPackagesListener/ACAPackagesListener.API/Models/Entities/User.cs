using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;

namespace ACAPackagesListener.API.Models.Entities
{
    [Serializable]
    public class User
    {
        public virtual Int32 UserId { get; set; }
        [Required]
        public virtual String Username { get; set; }
        [Required]
        public virtual String Password { get; set; }
        public virtual bool IsAdmin { get; set; }
    }
}
