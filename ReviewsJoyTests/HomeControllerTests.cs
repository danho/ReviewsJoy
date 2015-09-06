using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using ReviewsJoy.DAL;
using ReviewsJoyTests.TestDAL;
using ReviewsJoy.Controllers;

namespace ReviewsJoyTests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private IDatabaseContext db;
        private HomeController controller;

        [SetUp]
        public void SetUp()
        {
            db = new TestDatabaseContext();
            controller = new HomeController(db);
        }

        //[TestCase("a")]
        //[TestCase("street")]
        //public void LocationsGetByAddressShouldReturnLocations(string address)
        //{
        //    var locs = controller.LocationsGetByAddress(address);
        //    Assert.NotNull(locs);
        //    Assert.True(locs.Count > 0);
        //    foreach (var l in locs) { Assert.IsTrue(l.Address.Contains(address)); }
        //}
    }
}
