using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Persistance;
using NHibernate;
using NHibernate.Criterion;

namespace ACAPackagesListener.API.Models.Repositories
{
    public class NHParsedPackageRepository : NHWritableRepository<ParsedPackage>, IParsedPackageRepository
    {
        public IEnumerable<ParsedPackage> FromToday()
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            {
                var today = Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd"));
                return
                    session.CreateCriteria<ParsedPackage>()
                        .Add(Restrictions.Eq("Active", true))
                        .Add(Restrictions.Eq("PackageDate", today))
                        .AddOrder(new Order("PackageTimeOfDay", true))
                        .List<ParsedPackage>();
            }
        }

        public IEnumerable<ParsedPackage> FromDevices(IEnumerable<Int32> devices)
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            {
                return
                    session.CreateCriteria<ParsedPackage>()
                        .Add(Restrictions.In("DeviceId",devices.ToArray()))
                        .List<ParsedPackage>();
            }
        }

        public IEnumerable<ParsedPackage> GetByMacAndDates(string mac, int startDate, int endDate)
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            {
                return
                    session.CreateCriteria<ParsedPackage>()
                        .Add(Restrictions.Eq("MAC", mac))
                        .Add(Restrictions.Eq("Active", true))
                        .Add(Restrictions.Between("PackageDate", startDate, endDate))
                        .List<ParsedPackage>();
            }
        }
    }
}
