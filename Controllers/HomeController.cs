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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LandingPage()
        {
            return View();
        }

        public ActionResult Begin()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetLatestReviews()
        {
            return new ReviewsController(db).GetLatestReviews();
        }
    }
}