using System;
using System.Collections.Generic;
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
            ViewBag.Scripts = new List<String>{"device/edit.js"};
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
        public ActionResult Update(String DeviceId, String LatLng)
        {
            var device = _deviceRepository.GetById(Convert.ToInt32(DeviceId));
            device.LatLng = LatLng;
            _deviceRepository.Update(device);
            return Json(device, JsonRequestBehavior.AllowGet);
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
            }catch (Exception ex){
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }

        public ActionResult Delete(string DeviceId)
        {
            var m = "Dispositivo removido satisfactoriamente";
            try
            {
                var device = _deviceRepository.GetById(Convert.ToInt32(DeviceId));
                _deviceRepository.Remove(device);
            }
            catch (Exception ex)
            {
                m = "Error atendiendo la solicitud";
            }
            return Json(new { message = m }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string DeviceId)
        {
            ViewBag.HideNav = true;
            var device = _deviceRepository.GetById(Convert.ToInt32(DeviceId));
            return View("Edit", device);
        }
        [HttpPost]
        public ActionResult Edit(Device device)
        {
            try
            {
                _deviceRepository.Update(device);
                return Redirect("~/Device/Success");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }
    }
}
