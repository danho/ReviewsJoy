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
using ReviewsJoy.DAL.DTO;
using System.Threading;
using System.Threading.Tasks;
using ReviewsJoy.HelperMethods;
using System.Collections.Specialized;
using System.Text;

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
            return db.ReviewsGetMostRecent(placeId, 6);
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
                    generalReviews = reviews.Where(r => String.Equals(r.Category.Name, "General", StringComparison.OrdinalIgnoreCase)
                                                        && r.IsActive == true)
                                            .ToList();
                    categorizedReviews = reviews.Where(r => !String.Equals(r.Category.Name, "General", StringComparison.OrdinalIgnoreCase)
                                                        && r.IsActive == true)
                                            .ToList();
                }
            }
        }

        [HandleError]
        public ActionResult All(string placeId)
        {
            ViewBag.placeId = placeId;

            var locationTask = Task<GooglePlace>.Factory.StartNew(() => GetLocationDetails(placeId));

            var reviews = GetMostRecentReviews(placeId);
            if (reviews != null && reviews.Count > 0)
            {
                reviews.ForEach(r => r.Author = TextHelperMethods.UppercaseFirst(r.Author));
                ViewBag.locationId = reviews.FirstOrDefault().LocationId;
                ViewBag.Reviews = new JavaScriptSerializer().Serialize(reviews);
            }
            
            locationTask.Wait();
            ViewBag.Name = locationTask.Result.result.name;
            ViewBag.Address = locationTask.Result.result.formatted_address;

            return View();
        }

        [HttpPost]
        public JsonResult FilterByCategory(int locationId, string category)
        {
            return Json(db.ReviewsFilterByCategory(locationId, category, 6));
        }

        [ChildActionOnly]
        public GooglePlace GetLocationDetails(string placeId)
        {
            var key = WebConfigurationManager.AppSettings["GoogleServerKey"];
            var url = WebConfigurationManager.AppSettings["GoogleDetailsWebApiUrl"];

            var wc = new WebClient();
            wc.QueryString.Add("placeid", placeId);
            wc.QueryString.Add("key", key);
            var result = wc.DownloadString(url);
            var js = new JavaScriptSerializer();
            return (GooglePlace)js.Deserialize(result, typeof(GooglePlace));
        }

        [ChildActionOnly]
        public void AddReview(Review review)
        {
            db.AddReview(review);
        }

        [ChildActionOnly]
        public bool ValidateCaptcha(string captchaResponse)
        {
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["secret"] = WebConfigurationManager.AppSettings["GoogleCaptchaSecret"];
                values["response"] = captchaResponse;

                var response = client.UploadValues("https://www.google.com/recaptcha/api/siteverify", values);

                var responseString = Encoding.Default.GetString(response);
                if (!responseString.Contains("true"))
                    return false;
                else
                    return true;
            }
        }

        [HttpPost]
        public bool AddNewReview(int locationId, string placeId, string category, string review, string name, string locationName, string stars, string captchaResponse)
        {
            if (String.IsNullOrEmpty(captchaResponse))
                return false;

            if (!ValidateCaptcha(captchaResponse))
                return false;

            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(stars))
                return false;

            name = name.ToUpper();
            var numStars = Convert.ToInt32(stars);

            if (String.IsNullOrEmpty(category) ||
                category.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase))
            {
                category = "GENERAL";
            }
            category = category.ToUpper();
            var cat = db.CategoryGetByName(category);

            try
            {
                // No reviews exist
                // Create location and add review
                if (locationId == 0 && !String.IsNullOrEmpty(placeId))
                {
                    var newLoc = new Location { placeId = placeId, Name = locationName };
                    var locCtrl = new LocationController(db);
                    using (var scope = new TransactionScope())
                    {
                        newLoc = locCtrl.LocationAdd(newLoc);
                        var newReview = new Review
                        {
                            Location = newLoc,
                            ReviewText = review,
                            Category = cat,
                            Stars = numStars,
                            Author = name,
                            IsActive = true
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
                            Author = name,
                            Stars = numStars,
                            IsActive = true
                        };
                        AddReview(newReview);
                        scope.Complete();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [ChildActionOnly]
        public List<Review> ReviewsGetByCategoryName(int id, string categoryName)
        {
            return db.ReviewsGetByCategoryName(id, categoryName);
        }

        [HandleError]
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
            wc.QueryString.Add("types", "establishment");
            var result = wc.DownloadString(url);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetLatAndLng(string placeId)
        {
            var key = WebConfigurationManager.AppSettings["GoogleServerKey"];
            var url = WebConfigurationManager.AppSettings["GoogleDetailsWebApiUrl"];
            var wc = new WebClient();
            wc.QueryString.Add("placeid", placeId);
            wc.QueryString.Add("key", key);
            var result = wc.DownloadString(url);
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpVote(int Id)
        {
            return Json(db.UpVote(Id));
        }

        [HttpPost]
        public JsonResult DownVote(int Id)
        {
            return Json(db.DownVote(Id));
        }
    }
}
