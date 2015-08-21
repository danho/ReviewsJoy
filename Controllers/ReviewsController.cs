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
        public List<Review> ReviewsGetByLocationId(int locationId, int? count)
        {
            return db.ReviewsGetByLocationId(locationId, count);
        }

        [ChildActionOnly]
        public List<Review> ReviewsGeneralGetByLocationId(int locationId, int? count)
        {
            return db.ReviewsGeneralGetByLocationId(locationId, count);
        }

        [ChildActionOnly]
        public List<Review> ReviewsCategorizedGetByLocationId(int locationId, int? count)
        {
            return db.ReviewsCategorizedGetByLocationId(locationId, count);
        }

        public ActionResult All(int id)
        {
            ViewBag.id = id;
            ViewBag.GeneralReviews = ReviewsGeneralGetByLocationId(id, 10);
            ViewBag.CategorizedReviews = ReviewsCategorizedGetByLocationId(id, null);
            return View();
        }

        [ChildActionOnly]
        public void AddReview(Review review)
        {
            db.AddReview(review);
        }

        [HttpPost]
        public bool AddNewReview(int locationId, string name, string review)
        {
            try
            {
                var location = db.LocationGetById(locationId);
                if (location == null || String.IsNullOrEmpty(review))
                    return false;

                using (var scope = new TransactionScope())
                {
                    var newReview = new Review
                    {
                        Location = location,
                        Author = name,
                        ReviewText = review
                    };
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