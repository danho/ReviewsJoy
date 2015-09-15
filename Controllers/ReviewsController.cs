using ReviewsJoy.DAL;
using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        public ActionResult All(string placeId)
        {
            ViewBag.placeId = placeId;
            var locCtrl = new LocationController(db);
            var loc = locCtrl.LocationGetByPlaceId(placeId);
            if (loc != null)
            {
                var s = new JavaScriptSerializer();
                ViewBag.locationId = loc.LocationId;
                ViewBag.GeneralReviews = s.Serialize(ReviewsGeneralGetByLocationId(loc.LocationId, 10));
                ViewBag.CategorizedReviews = s.Serialize(ReviewsCategorizedGetByLocationId(loc.LocationId, null));
            }
            return View();
        }

        [ChildActionOnly]
        public void AddReview(Review review)
        {
            db.AddReview(review);
        }

        [HttpPost]
        public bool AddNewReview(int locationId, string placeId, string category, string review)
        {
            if (String.IsNullOrEmpty(category) ||
                category.Equals("general", StringComparison.InvariantCultureIgnoreCase))
            {
                category = "General";
            }
            var cat = db.CategoryGetByName(category);

            try
            {
                // No reviews exist
                // Create location and add review
                if (locationId == 0 && placeId != String.Empty)
                {
                    var newLoc = new Location { placeId = placeId };
                    var locCtrl = new LocationController(db);
                    using (var scope = new TransactionScope())
                    {
                        newLoc = locCtrl.LocationAdd(newLoc);
                        var newReview = new Review
                        {
                            Location = newLoc,
                            ReviewText = review,
                            Category = cat
                        };
                        AddReview(newReview);
                        scope.Complete();
                        return true;
                    }
                }
                // Reviews exist
                // Create review
                else
                {
                    var location = db.LocationGetById(locationId);
                    if (location == null || String.IsNullOrEmpty(review))
                        return false;

                    using (var scope = new TransactionScope())
                    {
                        if (cat == null)
                        {
                            cat = db.CategoryAdd(category);
                        }

                        var newReview = new Review
                        {
                            Location = location,
                            ReviewText = review,
                            Category = cat
                        };
                        AddReview(newReview);
                        scope.Complete();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        [ChildActionOnly]
        public List<Review> ReviewsGetByCategoryName(int id, string categoryName)
        {
            return db.ReviewsGetByCategoryName(id, categoryName);
        }

        public ActionResult ReviewsByCategoryName(int id, string categoryName)
        {
            var s = new JavaScriptSerializer();
            ViewBag.model = s.Serialize(ReviewsGetByCategoryName(id, categoryName));
            return View();
        }

        [HttpPost]
        public JsonResult AutoCompleteSearch(string searchText)
        {
            var key = WebConfigurationManager.AppSettings["GoogleServerKey"];
            var url = WebConfigurationManager.AppSettings["GooglePlacesWebApiUrl"];

            var wc = new WebClient();
            wc.QueryString.Add("input", searchText);
            wc.QueryString.Add("key", key);
            var result = wc.DownloadString(url);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetLatAndLng(string placeId)
        {
            var key = WebConfigurationManager.AppSettings["GoogleServerKey"];
            var url = "https://maps.googleapis.com/maps/api/place/details/json?placeid=" + placeId + "&key=" + key;
            var wc = new WebClient();
            var result = wc.DownloadString(url);
            return Json(result);
        }
    }
}