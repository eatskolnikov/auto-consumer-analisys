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

        private IEnumerable<ParsedPackage> GetByRange(Int32 start, Int32 finish, Int32 startTime = 0, Int32 endTime = 2400)
        {
            using (var session = NHibernateHelper.GetCurrentSession())
            {
                return new List<ParsedPackage>();
                //                  session.CreateSQLQuery("SELECT * FROM  ParsedPackages WHERE (PackageDate > {0} AND startTime > {1}",);
                /*
                    session.CreateCriteria<ParsedPackage>()
                        .Add(Restrictions.Eq("Activo", true))
                        .Add(Restrictions.And(new AndExpression(Restrictions.Gt("PackageDate", start),Restrictions.Gt("PackageTimeOfDay",startTime))))
                        .AddOrder(new Order("PackageTimeOfDay", true))
                        .List<ParsedPackage>();*/
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

        public IEnumerable<ParsedPackage> FromDateRange(Int32 start, Int32 finish, Int32 startTime=0, Int32 endTime=2400)
        {
            return GetByRange(start, finish, startTime, endTime);
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
