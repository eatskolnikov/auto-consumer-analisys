using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using ACAPackagesListener.API;
using ACAPackagesListener.API.Models.Enities;
using ACAPackagesListener.API.Models.Repositories;

namespace acawebclient
{
    public class BasePage : Page
    {
        protected User Identity;
        protected IDeviceRepository DeviceRepository { get; private set; }

        public BasePage()
        {
            DeviceRepository = new NHDeviceRepository();;
        }
    }
}