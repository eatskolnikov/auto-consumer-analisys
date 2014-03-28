using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Models.Repositories;

namespace acaweb.Controllers
{
    public class MapController : Controller
    {
        private IMapRepository _mapRepository;

        public MapController()
        {
            _mapRepository = new NHMapRepository();
        }

        public ActionResult Index()
        {
            var maps = _mapRepository.GetAll();

            return View(maps);
        }
        
        public ActionResult Add() { return View(); }

        [HttpPost]
        public ActionResult Add(Map user, HttpPostedFileBase mapfile)
        {
            try { _mapRepository.Add(user); }
            catch (Exception ex) { ModelState.AddModelError("", ex.Message); }
            return RedirectToAction("Index");
        }
    }
}
