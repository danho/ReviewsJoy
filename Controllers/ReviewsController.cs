using ReviewsJoy.DAL;
using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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

        public ActionResult One()
        {
            return View();
        }

        [ChildActionOnly]
        public List<Review> ReviewsGetByLocationId(int locationId)
        {
            return db.ReviewsGetByLocationId(locationId);
        }

        public ActionResult All(int id)
        {
            ViewBag.LocationId = id;
            return View(db.ReviewsGetByLocationId(id));
        }

        [ChildActionOnly]
        public void AddReview(Review review)
        {
            db.AddReview(review);
        }

        [HttpPost]
        public bool AddNewReview(int locationId, string review)
        {
            try
            {
                var location = db.LocationGetById(locationId);
                if (location == null || String.IsNullOrEmpty(review))
                    return false;

                using (var scope = new TransactionScope())
                {
                    var newReview = new Review { ReviewText = review };
                    AddReview(newReview);
                    scope.Complete();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}