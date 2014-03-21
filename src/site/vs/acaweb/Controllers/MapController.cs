using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACAPackagesListener.API.Models.Repositories;

namespace acaweb.Controllers
{
    public class MapController : Controller
    {
        private IMapRepository mapRepository;

        public MapController()
        {
            mapRepository = new NHMapRepository();
        }

        public ActionResult Index()
        {
            var maps = mapRepository.GetAll();

            return View(maps);
        }
        public ActionResult Add()
        {
            return View();
        }

    }
}
