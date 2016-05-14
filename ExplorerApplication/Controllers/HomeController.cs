using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ExplorerApplication.Models;

namespace ExplorerApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DirectoryModel model = new DirectoryModel(@"C:\"); 
            return View(model);
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