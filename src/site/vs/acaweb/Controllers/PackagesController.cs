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

        private class ReportTable
        {
            public String Descripcion { get; set; }
            public Int32 Piso { get; set; }
            public Int32 Personas { get; set; }
        }

        public ActionResult GetTable(string startDate = "", string endDate = "")
        {
            IEnumerable<ParsedPackage> packages;
            var deviceRepository = new NHDeviceRepository();
            if (!String.IsNullOrEmpty(startDate))
            {
                if (String.IsNullOrEmpty(endDate))
                    endDate = DateTime.Today.ToString("yyyyMMdd");
                packages = _parsedPackagesRepository.FromDateRange(DateTime.Parse(startDate), DateTime.Parse(endDate));
            }
            else { packages = _parsedPackagesRepository.GetAll(); }
            var tabla = new List<ReportTable>();
            foreach (var floor in packages.GroupBy(x=>x.Floor))
            {
                foreach(var item in floor.GroupBy(x=>x.LatLng))
                {
                    var device = deviceRepository.GetAll().FirstOrDefault(x => x.LatLng == item.Key);
                    tabla.Add(new ReportTable { Descripcion = device.Description, Piso = floor.Key, Personas = item.Count() });
                }
            }

            return Json(tabla, JsonRequestBehavior.AllowGet);
        }
    }
}
