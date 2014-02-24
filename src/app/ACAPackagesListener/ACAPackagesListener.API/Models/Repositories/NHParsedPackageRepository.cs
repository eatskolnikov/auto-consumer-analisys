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
        public void DeleteAll()
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.CreateSQLQuery("TRUNCATE TABLE ParsedPackages").ExecuteUpdate();
                    transaction.Commit();
                }
            }
        }

        public override IEnumerable<ParsedPackage> GetAll()
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            {
                return
                    session.CreateCriteria<ParsedPackage>()
                           .Add(Restrictions.Eq("Activo", true))
                           .AddOrder(new Order("MAC",true))
                           .AddOrder(new Order("PackageDateTime", true))
                           .List<ParsedPackage>();
            }
            
        }
        public IEnumerable<ParsedPackage> FromToday()
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            {
                var today = Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd"));
                return
                    session.CreateCriteria<ParsedPackage>()
                           .Add(Restrictions.Eq("Activo", true))
                           .Add(Restrictions.Eq("PackageDateTime", today))
                           .AddOrder(new Order("MAC", true))
                           .AddOrder(new Order("PackageDateTime", true))
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
                        .Add(Restrictions.Eq("PackageDateTime", day))
                        .AddOrder(new Order("MAC", true))
                        .AddOrder(new Order("PackageDateTime", true))
                        .List<ParsedPackage>();
            }
        }

        private IEnumerable<ParsedPackage> GetByRange(DateTime start, DateTime finish)
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            {
                return
                    session.CreateCriteria<ParsedPackage>()
                        .Add(Restrictions.Between("PackageDateTime", start, finish))
                        .AddOrder(new Order("MAC", true))
                        .AddOrder(new Order("PackageDateTime", true))
                        .List<ParsedPackage>();
            }
        }
        public IEnumerable<ParsedPackage> FromLastWeek()
        {
            var sevenDaysAgo = DateTime.Today.AddDays(-7);
            var today = DateTime.Today;
            return GetByRange(sevenDaysAgo, today);
        }

        public IEnumerable<ParsedPackage> FromLastMonth()
        {
            var aMonthAgo = DateTime.Today.AddMonths(-1);
            var today =DateTime.Today;
            return GetByRange(aMonthAgo, today);
        }

        public IEnumerable<ParsedPackage> FromDateRange(DateTime start, DateTime finish)
        {
            return GetByRange(start, finish);
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
