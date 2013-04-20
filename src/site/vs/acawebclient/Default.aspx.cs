using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace acawebclient
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if(Request.Cookies.AllKeys.Contains("LoggedUser"))
            {
                Response.Redirect("~/Home.aspx");
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (tbxUsername.Text == ConfigurationManager.AppSettings["TestUser"] && 
                tbxPassword.Text == ConfigurationManager.AppSettings["TestPass"])
            {
                Request.Cookies.Add(new HttpCookie("LoggedUser", "TestUser"));
            }else
            {
                lblMessage.Text = "Wrong username or password";
            }
        }
    }
}