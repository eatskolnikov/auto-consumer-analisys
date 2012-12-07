using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using ACAPackagesListener.API;
using Lrrc.Sys.Data;

namespace acawebclient
{
    public class BasePage : Page
    {
        protected Connection Connection;
        public BasePage()
        {
            Connection = new Connection(Globals.ConnectionString);
        }
    }
}