using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public ActionResult Get(string MAC = "")
        {
            var packages = _parsedPackagesRepository.GetAll();
            if(!String.IsNullOrEmpty(MAC))
            {
                packages = packages.Where(x => x.MAC == MAC);
            }
            return Json(packages.OrderBy(x => x.PackageDate), JsonRequestBehavior.AllowGet);
        }
    }
}
