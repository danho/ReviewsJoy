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
    public class CategoryControllerTests
    {
        private IDatabaseContext db;
        private CategoryController controller;

        [SetUp]
        public void SetUp()
        {
            db = new TestDatabaseContext();
            controller = new CategoryController(db);
        }

        [Test]
        public void CategoryGetAllShouldGetAllCategories()
        {
            var cats = db.CategoryGetAll();
            Assert.NotNull(cats);
            Assert.IsTrue(cats.Count > 0);
        }
    }
}
