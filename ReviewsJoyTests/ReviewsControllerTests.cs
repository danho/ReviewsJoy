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
            db = new TestDatabaseContext();
            controller = new ReviewsController(db);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void ReviewsGetByLocationIdShouldGetReviews(int locationId)
        {
            var locs = db.ReviewsGetByLocationId(locationId, null);
            Assert.NotNull(locs);
            Assert.IsTrue(locs.Count > 0);
            foreach (var l in locs) { Assert.IsTrue(l.Location.LocationId == locationId); }
        }

        [TestCase(3, 1, "a")]
        [TestCase(4, 2, "b")]
        public void AddReviewShouldAddReview(int reviewID, int locationId, string reviewText)
        {
            var newReview = new Review { ReviewId = reviewID, Location = db.LocationGetById(locationId),
                ReviewText = reviewText };
            controller.AddReview(newReview);
            var review = controller.ReviewsGetByLocationId(locationId).Last();
            Assert.IsNotNull(review);
            Assert.IsTrue(review.ReviewId == reviewID);
        }
    }
}
