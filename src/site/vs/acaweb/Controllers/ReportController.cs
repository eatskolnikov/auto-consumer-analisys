using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web.Mvc;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Models.Repositories;
using log4net.Appender;

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

        private class piepiece
        {
            public int value;
            public string color;
        }

        private class pie
        {

            public string[] labels;
            public string[] colors;
            public piepiece[] data;
        }

        private string intToHex(int n)
        {
            var s = n.ToString("X");
            if (s.Length == 1) s = "0" + s;
            return s;
        }

        public JsonResult ChartData(string startDate="", string endDate="", string startTime="", string endTime="")
        {
            IEnumerable<ParsedPackage> packages;
            if (!String.IsNullOrEmpty(startDate))
            {
                if (String.IsNullOrEmpty(endDate))
                    endDate = DateTime.Today.ToString("yyyyMMdd");

                packages = _parsedPackagesRepository.FromDateRange(Convert.ToInt32(startDate), Convert.ToInt32(endDate), Convert.ToInt32(startTime), Convert.ToInt32(endTime));
            }
            else
            {
                packages = _parsedPackagesRepository.GetAll();
            }
            var randonGen = new Random();

            var devices = _devicesRepository.GetAll().Where(x => x.Activo);
            var barLabels = new List<string>();
            var barData = new List<int>();
            var colors = new List<string>();
            var piePieces = new List<piepiece>();
            foreach (var device in devices)
            {
                var randomColor = Color.FromArgb(randonGen.Next(255), randonGen.Next(255),randonGen.Next(255));
                var currentColor = "#" + intToHex(randomColor.R) + intToHex(randomColor.G) + intToHex(randomColor.B);
                var packagesCount = packages.Count(x => x.LatLng == device.LatLng);
                barLabels.Add(device.Description);
                barData.Add(packagesCount);
                piePieces.Add(new piepiece { color = currentColor, value = packagesCount });
                colors.Add(currentColor);
            }
            var obj = new {
                bar = new bar
                {
                    labels = barLabels.ToArray(),
                    datasets = new []{
		                new bardataset{
		                    data = barData.ToArray()
		                }
                    }
                },
                pie = new pie
                {
                    colors = colors.ToArray(),
                    labels = barLabels.ToArray(),
                    data = piePieces.ToArray()
                }
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}
