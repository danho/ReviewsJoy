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

        [ChildActionOnly]
        public int LocationIdGetByPlaceId(string placeId)
        {
            return db.LocationIdGetByPlaceId(placeId);
        }

        [ChildActionOnly]
        public Location LocationGetByPlaceId(string placeId)
        {
            return db.LocationGetByPlaceId(placeId);
        }

        [ChildActionOnly]
        public Location LocationAdd(Location loc)
        {
            return db.LocationAdd(loc);
        }
    }
}