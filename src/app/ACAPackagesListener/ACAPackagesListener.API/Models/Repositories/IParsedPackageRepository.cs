﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACAPackagesListener.API.Models.Entities;

namespace ACAPackagesListener.API.Models.Repositories
{
    public interface IParsedPackageRepository : IWriteableCommonRepository<ParsedPackage>
    {
        IEnumerable<ParsedPackage> FromToday();
        IEnumerable<ParsedPackage> FromYesterday();
        IEnumerable<ParsedPackage> FromLastWeek();
        IEnumerable<ParsedPackage> FromLastMonth();
        IEnumerable<ParsedPackage> FromDateRange(DateTime startDate, DateTime finishDate);
        IEnumerable<ParsedPackage> FromDevices(IEnumerable<Int32> devices);
        void DeleteAll();
    }
}
