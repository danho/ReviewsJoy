using ReviewsJoy.DAL;
using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewsJoy.Controllers
{
    public class LocationController : Controller
    {
        private IDatabaseContext db;

        public LocationController(IDatabaseContext db)
        {
            this.db = db;
        }

        [ChildActionOnly]
        public Location LocationGetById(int id)
        {
            return db.LocationGetById(id);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(LocationGetById(id));
        }

        [ChildActionOnly]
        public List<Location> LocationsGetByAddress(string address)
        {
            return db.LocationGetByAddress(address);
        }

        public ActionResult GetLocationsByAddressWebService(string address)
        {
            return View(LocationsGetByAddress(address));
        }
    }
}