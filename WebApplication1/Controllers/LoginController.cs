﻿using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.DBA;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName,PassWord")] Users users)
        {
            if (ModelState.IsValid)
            {
                if(users.UserName=="admin" && users.PassWord == "admin")
                {
                    FormsAuthentication.SetAuthCookie(users.UserName, false);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError("Error", "Wrong Username or Password");
                    return View();
                }


            }
            return View(users);
        }
    }
}