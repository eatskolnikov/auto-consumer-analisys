﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Models.Repositories;

namespace acaweb.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;
        public DeviceController()
        {
            _deviceRepository = new NHDeviceRepository();
        }

        public ActionResult Index()
        {
            ViewBag.HasMap = true;
            ViewBag.Script = "edit.js";
            return View();
        }

        public ActionResult Success()
        {
            ViewBag.HideNav = true;
            return View();
        }

        public ActionResult Get()
        {
            var devices = _deviceRepository.GetAll();
            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(String LatLng)
        {
            ViewBag.HideNav = true;
            return View("Add", new Device { LatLng = LatLng });
        }
        [HttpPost]
        public ActionResult Add(Device device)
        {
            try{
                _deviceRepository.Add(device);
                return Redirect("~/Device/Success");
            }catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }
    }
}
