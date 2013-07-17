using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ACAPackagesListener.API.Authentication;
using acaweb.Models;

namespace acaweb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authenticator;

        public AccountController()
        {
            _authenticator = new HardCodeAuthenticator();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        //
        // GET: /Account/Login
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (_authenticator.Authenticate(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    if (Request.Params.Count > 0 && Request.Params["ReturnUrl"] != null)
                    {
                        return Redirect(Request.Params["ReturnUrl"]);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username or password");
            }
            return View();
        }
        //
        // GET: /Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
