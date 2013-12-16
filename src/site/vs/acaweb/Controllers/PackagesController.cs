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
            return Json(packages, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHeat(string MAC = "", string startDate = "", string endDate = "", string startTime = "", string endTime = "")
        {
            IEnumerable<ParsedPackage> packages;

            if (!String.IsNullOrEmpty(startDate))
            {
                if (String.IsNullOrEmpty(endDate))
                    endDate = DateTime.Today.ToString("yyyyMMdd");
                packages = _parsedPackagesRepository.FromDateRange(Convert.ToInt32(startDate), Convert.ToInt32(endDate), Convert.ToInt32(startTime), Convert.ToInt32(endTime));
            }
            else { packages = _parsedPackagesRepository.GetAll(); }
            if (!String.IsNullOrEmpty(MAC)){packages = packages.Where(x => x.MAC == MAC);}
            var result = packages.GroupBy(x => x.LatLng);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(string MAC = "", string startDate = "", string endDate = "", string startTime = "", string endTime = "")
        {
            IEnumerable<ParsedPackage> packages;
            if (!String.IsNullOrEmpty(startDate)) {
                if(String.IsNullOrEmpty(endDate))
                    endDate = DateTime.Today.ToString("yyyyMMdd");
                packages = _parsedPackagesRepository.FromDateRange(Convert.ToInt32(startDate), Convert.ToInt32(endDate), Convert.ToInt32(startTime), Convert.ToInt32(endTime));
            }
            else { packages = _parsedPackagesRepository.GetAll(); }
            if(!String.IsNullOrEmpty(MAC))
            {
                packages = packages.Where(x => x.MAC == MAC);
            }
            return Json(packages.OrderBy(x => x.PackageDate), JsonRequestBehavior.AllowGet);
        }
    }
}
