using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace acaweb.Controllers
{
    public class BaseController : Controller
    {
        public virtual ActionResult Index()
        {
            throw new NotImplementedException("This is an invalid route");
        }
    }
}
