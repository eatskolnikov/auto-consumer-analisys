using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ACAPackagesListener.API.Authentication;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Models.Repositories;
using acaweb.Models;

namespace acaweb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authenticator;
        private readonly IUserRepository _userRepository;

        public AccountController()
        {
            _authenticator = new DatabaseAuthentication();
            _userRepository = new NHUserRepository();
        }
        public ActionResult Add() { return View(); }
        [HttpPost]
        public ActionResult Add(User user)
        {
            try { _userRepository.Add(user); }
            catch (Exception ex) { ModelState.AddModelError("", ex.Message); }
            return View();
        }
        public ActionResult Edit(){ return View(); }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try {
                _userRepository.Update(user);
                return Redirect("~/Device/Success");
            }
            catch (Exception ex) { ModelState.AddModelError("", ex.Message); }
            return View();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
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
                if (_authenticator.Authenticate(model.Username.ToLower(), model.Password))
                {
                    var user = _userRepository.Authenticate(model.Username.ToLower(), model.Password);
                    Session.Add("UserData", user);
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
