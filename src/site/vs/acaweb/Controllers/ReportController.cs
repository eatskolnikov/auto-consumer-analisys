using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ACAPackagesListener.API.Models.Repositories;

namespace acaweb.Controllers
{
    public class ReportController : Controller
    {
        private readonly IParsedPackageRepository _parsedPackagesRepository;
        private readonly IDeviceRepository _devicesRepository;

        public ReportController()
        {
            _parsedPackagesRepository = new NHParsedPackageRepository();
            _devicesRepository = new NHDeviceRepository();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Heat()
        {
            ViewBag.HasMap = true;
            ViewBag.Scripts = new List<string>{"heat-report.js", "filter-report.js"} ;
            return View();
        }

        public ActionResult Routes()
        {
            ViewBag.HasMap = true;
            ViewBag.Scripts = new List<string> { "routes-report.js", "filter-report.js" };
            return View();
        }

        public ActionResult Charts()
        {
            ViewBag.HasMap = true;
            ViewBag.Scripts = new List<string> { "Chart.min.js", "chart-report.js", "filter-report.js" };
            return View();
        }

        private class bardataset
        {
            public string fillColor = "rgba(220,220,220,0.5)";
            public string strokeColor = "rgba(220,220,220,1)";
            public int[] data;
        }

        private class bar
        {
            public string[] labels;
            public bardataset[] datasets;
        }
        public JsonResult ChartData(int from, int to)
        {
            var packages = _parsedPackagesRepository.GetAll().Where(x => x.PackageDate >= from && x.PackageDate <= to);
            var devices = _devicesRepository.GetAll().Where(x => x.Activo);
            var barLabels = new List<string>();
            var barData = new List<int>();
            foreach (var device in devices)
            {
                barLabels.Add(device.Description);
                barData.Add(packages.Count(x=>x.LatLng == device.LatLng));
            }
            var obj = new { 
                bar=new bar{
                    labels = barLabels.ToArray(),
                    datasets = new []{
		                new bardataset{
		                    data = barData.ToArray()
		                }
                    }
                }
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}
