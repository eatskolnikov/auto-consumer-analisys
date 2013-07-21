using System.Collections.Generic;
using System.Web.Mvc;

namespace acaweb.Controllers
{
    public class ReportController : Controller
    {
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
    }
}
