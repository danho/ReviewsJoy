using NUnit.Framework;
using ReviewsJoy.Controllers;
using ReviewsJoy.DAL;
using ReviewsJoy.Models;
using ReviewsJoyTests.TestDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsJoyTests
{
    [TestFixture]
    public class ReviewsControllerTests
    {
        private IDatabaseContext db;
        private ReviewsController controller;

        [SetUp]
        public void SetUp()
        {
            db = new TestDatabaseContext().GetMockDatabase();
            var unitOfWork = new UnitOfWork(db);
            controller = new ReviewsController(unitOfWork);
        }

        [TestCase("a")]
        [TestCase("b")]
        public void GetMostRecentReviewsShouldGetReviews(string locationId)
        {
            var reviews = controller.GetMostRecentReviews("a");
            Assert.NotNull(reviews);
            Assert.True(reviews.Count > 0);
        }

        //[TestCase(1)]
        //[TestCase(2)]
        //public void ReviewsGetByLocationIdShouldGetReviews(int locationId)
        //{
        //    var locs = controller.ReviewsGetByLocationId(locationId, null);
        //    Assert.NotNull(locs);
        //    Assert.IsTrue(locs.Count > 0);
        //    foreach (var l in locs) { Assert.IsTrue(l.Location.LocationId == locationId); }
        //}

        //[TestCase(1, "a")]
        //[TestCase(2, "b")]
        //public void AddReviewShouldAddReview(int locationId, string reviewText)
        //{
        //    var newReview = new Review
        //    {
        //        Location = db.LocationGetById(locationId),
        //        ReviewText = reviewText
        //    };
        //    controller.AddReview(newReview);
        //    var review = controller.ReviewsGetByLocationId(locationId, null).Last();
        //    Assert.IsNotNull(review);
        //}

        //[TestCase(1, null)]
        //public void ReviewsGeneralGetByLocationIdShouldGetGeneralReviews(int locationId, int? count)
        //{
        //    var reviews = controller.ReviewsGeneralGetByLocationId(locationId, count);
        //    Assert.NotNull(reviews);
        //    Assert.IsTrue(reviews.Count > 0);
        //    foreach (var review in reviews)
        //        Assert.IsTrue(String.Equals(review.Category.Name, "General", StringComparison.OrdinalIgnoreCase));
        //}

        //[TestCase(2, null)]
        //public void ReviewsCategorizedGetByLocationIdShouldGetCategorizedReviews(int locationId, int? count)
        //{
        //    var reviews = controller.ReviewsCategorizedGetByLocationId(locationId, count);
        //    Assert.NotNull(reviews);
        //    Assert.IsTrue(reviews.Count > 0);
        //    foreach (var review in reviews)
        //        Assert.IsTrue(!String.Equals(review.Category.Name, "General", StringComparison.OrdinalIgnoreCase));
        //}

        //[TestCase("b")]
        //public void GetAllReviewsShouldGetAllReviewsForLocation(string placeId)
        //{
        //    int locationId = 0;
        //    List<Review> generalReviews = null;
        //    List<Review> categorizedReviews = null;
        //    controller.GetAllReviews(placeId, out locationId, out generalReviews, out categorizedReviews);
        //    Assert.IsTrue(locationId != 0);
        //    Assert.NotNull(generalReviews);
        //    Assert.NotNull(categorizedReviews);
        //    Assert.IsTrue(generalReviews.Count > 0);
        //    Assert.IsTrue(categorizedReviews.Count > 0);
        //}

        //[TestCase(2, "Food")]
        //public void ReviewsGetByCategoryNameShouldGetReview(int locationId, string categoryName)
        //{
        //    var reviews = controller.ReviewsGetByCategoryName(locationId, categoryName);
        //    Assert.NotNull(reviews);
        //    foreach (var review in reviews)
        //        Assert.IsTrue(review.Category.Name.Equals(categoryName));
        //}
    }
}
