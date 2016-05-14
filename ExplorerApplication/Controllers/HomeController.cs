using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ExplorerApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DriveInfo[] drivers = DriveInfo.GetDrives();
            ViewBag.test = drivers;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}