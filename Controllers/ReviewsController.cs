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
using ReviewsJoy.DAL.Repository;
using System.Data.Entity;

namespace ReviewsJoy.Controllers
{
    public class ReviewsController : Controller
    {
        private IUnitOfWork unitOfWork;

        public ReviewsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [ChildActionOnly]
        public List<ReviewDTO> GetMostRecentReviews(string placeId)
        {
            var results = unitOfWork.ReviewsRepository.Get(r => r.Location.placeId == placeId && r.IsActive == true, x => x.OrderByDescending(r => r.UpVotes - r.DownVotes).ThenByDescending(r => r.ReviewId));
            return results.Take(6)
                            .Select(r => new ReviewDTO
                            {
                                Id = r.ReviewId,
                                Author = r.Author,
                                CategoryName = r.Category.Name,
                                LocationId = r.Location.LocationId,
                                ReviewText = r.ReviewText,
                                Stars = r.Stars,
                                UpVotes = r.UpVotes,
                                DownVotes = r.DownVotes
                            })
                                .ToList();
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
            var test = unitOfWork.ReviewsRepository.Get(r => r.Location.LocationId == locationId && r.Category.Name.Contains(category) && r.IsActive == true).ToList();
            var results = unitOfWork.ReviewsRepository.Get(r => r.Location.LocationId == locationId && r.Category.Name.Contains(category) && r.IsActive == true
                                                            )
                                                                .Take(6);
            return Json(results
                            .Select(r => new ReviewDTO
                                {
                                    Id = r.ReviewId,
                                    Author = r.Author,
                                    CategoryName = r.Category.Name,
                                    LocationId = r.Location.LocationId,
                                    ReviewText = r.ReviewText,
                                    Stars = r.Stars,
                                    UpVotes = r.UpVotes,
                                    DownVotes = r.DownVotes
                                }));
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
            unitOfWork.ReviewsRepository.context.Entry<Location>(review.Location).State = EntityState.Unchanged;
            unitOfWork.ReviewsRepository.Insert(review);
            unitOfWork.Save();
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
            var cat = unitOfWork.CategoryRepository.Get(c => c.Name.Equals(category, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            try
            {
                // No reviews exist
                // Create location and add review
                if (locationId == 0 && !String.IsNullOrEmpty(placeId))
                {
                    var newLoc = new Location { placeId = placeId, Name = locationName };
                    using (var scope = new TransactionScope())
                    {
                        newLoc = unitOfWork.LocationRepository.Insert(newLoc);
                        unitOfWork.Save();
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
                    var location = unitOfWork.LocationRepository.Get(l => l.LocationId == locationId).FirstOrDefault();
                    if (location == null || String.IsNullOrEmpty(review))
                        return false;

                    using (var scope = new TransactionScope())
                    {
                        if (cat == null)
                        {
                            cat = unitOfWork.CategoryRepository.Insert(new Category { Name = category });
                            unitOfWork.Save();
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
            catch
            {
                return false;
            }
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
            var review = unitOfWork.ReviewsRepository.Get(r => r.ReviewId == Id).FirstOrDefault();
            review.UpVotes++;
            unitOfWork.ReviewsRepository.Update(review);
            unitOfWork.Save();
            return Json(new ReviewDTO
            {
                Id = review.ReviewId,
                Author = review.Author,
                CategoryName = review.Category.Name,
                LocationId = review.Location.LocationId,
                ReviewText = review.ReviewText,
                Stars = review.Stars,
                UpVotes = review.UpVotes,
                DownVotes = review.DownVotes
            });
        }

        [HttpPost]
        public JsonResult DownVote(int Id)
        {
            var review = unitOfWork.ReviewsRepository.Get(r => r.ReviewId == Id).FirstOrDefault();
            review.DownVotes++;
            unitOfWork.ReviewsRepository.Update(review);
            unitOfWork.Save();
            return Json(new ReviewDTO
            {
                Id = review.ReviewId,
                Author = review.Author,
                CategoryName = review.Category.Name,
                LocationId = review.Location.LocationId,
                ReviewText = review.ReviewText,
                Stars = review.Stars,
                UpVotes = review.UpVotes,
                DownVotes = review.DownVotes
            });
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
