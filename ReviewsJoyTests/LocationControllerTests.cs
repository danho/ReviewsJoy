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
    }
}
