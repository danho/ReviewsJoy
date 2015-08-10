using NUnit.Framework;
using ReviewsJoy.Controllers;
using ReviewsJoy.DAL;
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
            var locs = db.ReviewsGetByLocationId(locationId);
            Assert.NotNull(locs);
            Assert.IsTrue(locs.Count > 0);
            foreach (var l in locs) { Assert.IsTrue(l.Location.LocationId == locationId); }
        }
    }
}
