using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ACAPackagesListener.API;
using ACAPackagesListener.API.Authentication;
using ACAPackagesListener.API.Models.Entities;
using acaweb.Models;

namespace acaweb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticate _authenticator;
        public LoginController()
        {
            _authenticator = new HardCodeAuthenticator();
        }
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                if(_authenticator.Authenticate(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    if(Request.Params.Count> 0 && Request.Params["ReturnUrl"]!=null)
                    {
                        return Redirect(Request.Params["ReturnUrl"]);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username or password");
            }
            return View();
        }
    }
}
