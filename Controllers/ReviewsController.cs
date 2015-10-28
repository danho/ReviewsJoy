﻿using ReviewsJoy.DAL;
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
using ReviewsJoy.DAL.DTO;

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

        [ChildActionOnly]
        public List<ReviewDTO> GetMostRecentReviews(string placeId)
        {
            return db.ReviewsGetMostRecent(placeId, 10);
        }

        [ChildActionOnly]
        public void GetAllReviews(string placeId, out int locationId, out List<Review> generalReviews, out List<Review> categorizedReviews)
        {
            locationId = 0;
            generalReviews = null;
            categorizedReviews = null;

            if (!String.IsNullOrEmpty(placeId))
            {
                var reviews = db.ReviewsGetAll(placeId);
                if (reviews.Count > 0)
                {
                    locationId = reviews.FirstOrDefault().Location.LocationId;
                    generalReviews = reviews.Where(r => String.Equals(r.Category.Name, "General", StringComparison.OrdinalIgnoreCase))
                                            .ToList();
                    categorizedReviews = reviews.Where(r => !String.Equals(r.Category.Name, "General", StringComparison.OrdinalIgnoreCase))
                                            .ToList();
                }
            }
        }

        public ActionResult All(string placeId)
        {
            ViewBag.placeId = placeId;

            var key = WebConfigurationManager.AppSettings["GoogleServerKey"];
            var url = WebConfigurationManager.AppSettings["GoogleDetailsWebApiUrl"];

            var wc = new WebClient();
            wc.QueryString.Add("placeid", placeId);
            wc.QueryString.Add("key", key);
            var result = wc.DownloadString(url);
            GooglePlace gp = (GooglePlace)new JavaScriptSerializer().Deserialize(result, typeof(GooglePlace));

            ViewBag.Name = gp.result.name;
            ViewBag.Address = gp.result.formatted_address;
            

            var reviews = GetMostRecentReviews(placeId);
            if (reviews != null && reviews.Count > 0)
            {
                ViewBag.locationId = reviews.FirstOrDefault().LocationId;
                ViewBag.Reviews = new JavaScriptSerializer().Serialize(reviews);
            }
            return View();
        }

        [ChildActionOnly]
        public void AddReview(Review review)
        {
            db.AddReview(review);
        }

        [HttpPost]
        public bool AddNewReview(int locationId, string placeId, string category, string review, string name)
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
                            Category = cat,
                            Author = name
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

        [ChildActionOnly]
        public void WarmUpDb()
        {
            db.WarmUpDb();
        }
    }
}