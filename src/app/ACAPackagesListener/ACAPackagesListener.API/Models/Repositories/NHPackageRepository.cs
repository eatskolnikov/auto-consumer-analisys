﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACAPackagesListener.API.Models.Entities;

namespace ACAPackagesListener.API.Models.Repositories
{
    public class NHPackageRepository : NHWritableRepository<Package>, IPackageRepository
    {
    }
}
