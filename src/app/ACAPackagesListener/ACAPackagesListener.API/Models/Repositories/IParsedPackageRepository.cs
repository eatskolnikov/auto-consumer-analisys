using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACAPackagesListener.API.Models.Entities;

namespace ACAPackagesListener.API.Models.Repositories
{
    public interface IParsedPackageRepository : IWriteableCommonRepository<ParsedPackage>
    {
        IEnumerable<ParsedPackage> FromToday();
        IEnumerable<ParsedPackage> FromDevices(IEnumerable<Int32> devices);
        IEnumerable<ParsedPackage> GetByMacAndDates(string mac, int startDate, int endDate);
    }
}
