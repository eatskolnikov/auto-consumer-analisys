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
            ViewBag.Script = "heat-report.js";
            return View();
        }

        public ActionResult Routes()
        {
            ViewBag.HasMap = true;
            ViewBag.Script = "routes-report.js";
            return View();
        }
    }
}
