using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Models.Repositories;

namespace acaweb.Controllers
{
    public class PackagesController : Controller
    {
        private readonly IParsedPackageRepository _parsedPackagesRepository;

        public PackagesController()
        {
            _parsedPackagesRepository = new NHParsedPackageRepository();
        }

        public ActionResult Index()
        {
            var packages = _parsedPackagesRepository.GetAll();
            return Json(packages, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Filter()
        {
            return View();
        }

        public ActionResult GetHeat(string report = "", string MAC = "")
        {
            IEnumerable<ParsedPackage> packages;

            /*switch (report)
            {
                case "today":
                    packages = _parsedPackagesRepository.FromToday().GroupBy(x=>x.LatLng);
                    break;
                case "yesterday":
                    packages = _parsedPackagesRepository.FromYesterday().GroupBy(x=>x.LatLng);
                    break;
                case "weekly":
                    packages = _parsedPackagesRepository.FromLastWeek().GroupBy(x=>x.LatLng);
                    break;
                case "monthly":
                    packages = _parsedPackagesRepository.FromLastMonth().GroupBy(x=>x.LatLng);
                    break;
                default:
                    packages = _parsedPackagesRepository.FromToday().GroupBy(x=>x.LatLng);
                    break;
            }*/
            packages = _parsedPackagesRepository.GetAll();
            if (!String.IsNullOrEmpty(MAC))
            {
                packages = packages.Where(x => x.MAC == MAC);
            }
            var result = packages.GroupBy(x => x.LatLng);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(string report="", string MAC = "")
        {
            IEnumerable<ParsedPackage> packages;
            
            /*switch (report)
            {
                case "today":
                    packages = _parsedPackagesRepository.FromToday();
                    break;
                case "yesterday":
                    packages = _parsedPackagesRepository.FromYesterday();
                    break;
                case "weekly":
                    packages = _parsedPackagesRepository.FromLastWeek();
                    break;
                case "monthly":
                    packages = _parsedPackagesRepository.FromLastMonth();
                    break;
                default:
                    packages = _parsedPackagesRepository.FromToday();
                    break;
            }*/
            packages = _parsedPackagesRepository.GetAll();
            if(!String.IsNullOrEmpty(MAC))
            {
                packages = packages.Where(x => x.MAC == MAC);
            }
            return Json(packages.OrderBy(x => x.PackageDate), JsonRequestBehavior.AllowGet);
        }
    }
}
