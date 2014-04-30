using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Models.Repositories;
using NHibernate.Criterion;

namespace acaweb.Controllers
{
    public class PackagesController : Controller
    {
        private readonly IParsedPackageRepository _parsedPackagesRepository;

        public PackagesController()
        {
            _parsedPackagesRepository = new NHParsedPackageRepository();
        }

        public JsonResult ClearData()
        {
            _parsedPackagesRepository.DeleteAll();
            return Json(new {success = true}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var packages = _parsedPackagesRepository.GetAll();
            return Json(packages.GroupBy(x=>x.MAC), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHeat(string startDate = "", string endDate = "", int floor=1)
        {
            IEnumerable<ParsedPackage> packages;

            if (!String.IsNullOrEmpty(startDate))
            {
                if (String.IsNullOrEmpty(endDate))
                    endDate = DateTime.Today.ToString("yyyyMMdd");
                packages = _parsedPackagesRepository.FromDateRange(DateTime.Parse(startDate), DateTime.Parse(endDate)).Where(x => x.Floor == floor);
            }
            else { packages = _parsedPackagesRepository.GetAll().Where(x => x.Floor == floor); }
            var result = packages.GroupBy(x => x.LatLng);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(string startDate = "", string endDate = "", int floor=1)
        {
            IEnumerable<ParsedPackage> packages;
            if (!String.IsNullOrEmpty(startDate)) {
                if(String.IsNullOrEmpty(endDate))
                    endDate = DateTime.Today.ToString("yyyyMMdd");
                packages = _parsedPackagesRepository.FromDateRange(DateTime.Parse(startDate), DateTime.Parse(endDate)).Where(x => x.Floor == floor);
            }
            else { packages = _parsedPackagesRepository.GetAll().Where(x=>x.Floor == floor); }
            return Json(packages.GroupBy(x => x.MAC), JsonRequestBehavior.AllowGet);
        }
    }
}
