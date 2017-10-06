using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DBA;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
      [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

    }
}
