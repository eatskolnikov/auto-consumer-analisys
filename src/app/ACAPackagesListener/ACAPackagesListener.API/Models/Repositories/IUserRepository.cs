using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACAPackagesListener.API.Models.Enities;

namespace ACAPackagesListener.API.Models.Repositories
{
    public interface IUserRepository : IWriteableCommonRepository<User>
    {
        User Authenticate(string username, string password);
    }
}
