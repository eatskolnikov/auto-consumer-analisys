using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace acaweb.Controllers
{
    public class BaseMapController : Controller
    {
        public BaseMapController()
        {
            ViewBag.HasMap = true;
        }

        public virtual ActionResult Index()
        {
            throw new NotImplementedException("This is an invalid route");
        }
    }
}
