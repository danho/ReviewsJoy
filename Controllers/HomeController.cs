using ReviewsJoy.DAL;
using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewsJoy.Controllers
{
    public class HomeController : Controller
    {
        private IDatabaseContext db;

        public HomeController(IDatabaseContext db)
        {
            this.db = db;
        }

        [RequireHttps]
        public ActionResult Index()
        {
            var cats = db.CategoryGetAll();
            return View();
        }

        [ChildActionOnly]
        public List<Location> LocationsGetByAddress(string address)
        {
            return db.LocationGetByAddress(address);
        }

        public JsonResult GetLocationsByAddressWebService(string address)
        {
            return Json(LocationsGetByAddress(address), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LandingPage()
        {
            return View();
        }

        public ActionResult Begin()
        {
            return View();
        }
    }
}