using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Sample(string k)
        {
            return RedirectToAction("Contact");
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Landing page";

            return View();
        }
        [HttpPost]
        public ActionResult About(string btnSubmit)
        {
            if (btnSubmit == "Save")
                ViewBag.Message = "Save triggered";
            else
                ViewBag.Message = "Search triggered";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}