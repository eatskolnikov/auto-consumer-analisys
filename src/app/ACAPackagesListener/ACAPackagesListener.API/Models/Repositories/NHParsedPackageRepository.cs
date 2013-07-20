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
                           .Add(Restrictions.Eq("Activo", true))
                           .Add(Restrictions.Eq("PackageDate", today))
                           .AddOrder(new Order("PackageTimeOfDay", true))
                           .List<ParsedPackage>();
            }
        }
        public IEnumerable<ParsedPackage> FromYesterday()
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            {
                var day = Convert.ToInt32(DateTime.Today.AddDays(-1).ToString("yyyyMMdd"));
                return
                    session.CreateCriteria<ParsedPackage>()
                        .Add(Restrictions.Eq("Activo", true))
                        .Add(Restrictions.Eq("PackageDate", day))
                        .AddOrder(new Order("PackageTimeOfDay", true))
                        .List<ParsedPackage>();
            }
        }

        private IEnumerable<ParsedPackage> GetByRange(Int32 start, Int32 finish)
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            {
                return
                    session.CreateCriteria<ParsedPackage>()
                        .Add(Restrictions.Eq("Activo", true))
                        .Add(Restrictions.Between("PackageDate", start, finish))
                        .AddOrder(new Order("PackageTimeOfDay", true))
                        .List<ParsedPackage>();
            }
        }
        public IEnumerable<ParsedPackage> FromLastWeek()
        {
            var sevenDaysAgo = Convert.ToInt32(DateTime.Today.AddDays(-7).ToString("yyyyMMdd"));
            var today = Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd"));
            return GetByRange(sevenDaysAgo, today);
        }

        public IEnumerable<ParsedPackage> FromLastMonth()
        {
            var aMonthAgo = Convert.ToInt32(DateTime.Today.AddMonths(-1).ToString("yyyyMMdd"));
            var today = Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd"));
            return GetByRange(aMonthAgo, today);
        }

        public IEnumerable<ParsedPackage> FromDateRange(Int32 start, Int32 finish)
        {
            var aMonthAgo = Convert.ToInt32(DateTime.Today.AddMonths(-1).ToString("yyyyMMdd"));
            var today = Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd"));
            return GetByRange(aMonthAgo, today);
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
    }
}
