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
    public class LocationControllerTests
    {
        private IDatabaseContext db;
        private LocationController controller;

        [SetUp]
        public void SetUp()
        {
            db = new TestDatabaseContext().GetMockDatabase();
            controller = new LocationController(db);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void LocationsGetByIdShouldReturnLocation(int id)
        {
            var loc = controller.LocationGetById(id);
            Assert.NotNull(loc);
            Assert.IsTrue(loc.LocationId == id);
        }

        [TestCase("b")]
        public void LocationGetByPlaceIdShouldReturnLocation(string placeId)
        {
            var loc = controller.LocationGetByPlaceId(placeId);
            Assert.NotNull(loc);
            Assert.IsTrue(loc.placeId.Equals(placeId));
        }

        [TestCase("b")]
        public void LocationIdGetByPlaceIdShouldReturnLocationId(string placeId)
        {
            var locId = controller.LocationIdGetByPlaceId(placeId);
            Assert.IsTrue(locId != 0);
        }

        public void LocationAddShouldAddLocation(Location loc)
        {
            var newLoc = controller.LocationAdd(new Location { placeId = "c" });
            Assert.NotNull(newLoc);
            Assert.IsTrue(newLoc.placeId == "c");
        }
    }
}
