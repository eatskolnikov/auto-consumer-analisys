using System;
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
            var devices = _deviceRepository.GetAll();
            return Json(devices, JsonRequestBehavior.AllowGet);
        }
    }
}
