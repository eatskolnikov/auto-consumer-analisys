﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace acawebclient.master
{
    public partial class Logged : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty((string) Session["LoggedUser"]))
                Response.Redirect("~/Default.aspx");
        }
    }
}