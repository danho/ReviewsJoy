using ReviewsJoy.DAL;
using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewsJoy.Controllers
{
    public class ReviewsController : Controller
    {
        private IDatabaseContext db;

        public ReviewsController(IDatabaseContext db)
        {
            this.db = db;
        }

        [ChildActionOnly]
        public List<Review> ReviewsGetByLocationId(int locationId)
        {
            return db.ReviewsGetByLocationId(locationId);
        }

        public ActionResult All(int id)
        {
            return View(db.ReviewsGetByLocationId(id));
        }
    }
}