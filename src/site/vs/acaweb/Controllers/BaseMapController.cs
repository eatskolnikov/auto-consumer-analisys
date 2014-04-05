using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACAPackagesListener.API.Models.Repositories;

namespace acaweb.Controllers
{
    public class BaseMapController : Controller
    {
        protected IMapRepository mapRepository;

        public BaseMapController()
        {
            //mapRepository = new NHMapRepository();
            //var selected = mapRepository.GetSelected();
            ViewBag.TilesSource = "Content/maps/8240df3408ae029b955bcd29743dec98/";// selected.TilesSource;
        }
    }
}