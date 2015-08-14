using ReviewsJoy.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewsJoy.Models;

namespace ReviewsJoyTests.TestDAL
{
    public class TestDatabaseContext : IDatabaseContext
    {

        private List<Location> locations;
        private List<Category> categories;
        private List<Review> reviews;

        public TestDatabaseContext()
        {
            var l1 = new Location
            {
                LocationId = 1,
                Address = "a street",
                City = "New York",
                State = "NY",
                Zip = "10000",
                Name = "l1",
                XCoordinate = 25.2,
                YCoordinate = 30.1
            };

            var l2 = new Location
            {
                LocationId = 2,
                Address = "b street",
                City = "New York",
                State = "NY",
                Zip = "10001",
                Name = "l2",
                XCoordinate = 22.2,
                YCoordinate = 32.1
            };

            locations = new List<Location>();
            locations.Add(l1);
            locations.Add(l2);

            var c1 = new Category
            {
                CategoryId = 1,
                Name = "Food"
            };
            var c2 = new Category
            {
                CategoryId = 2,
                Name = "Parking"
            };

            categories = new List<Category>();
            categories.Add(c1);
            categories.Add(c2);

            var r1 = new Review
            {
                ReviewId = 1,
                Author = "A",
                Category = c1,
                Location = l1,
                ReviewText = "Pretty good.",
                Stars = 8
            };
            var r2 = new Review
            {
                ReviewId = 2,
                Author = "B",
                Category = c2,
                Location = l2,
                ReviewText = "Plenty of parking spaces",
                Stars = 10
            };

            reviews = new List<Review>();
            reviews.Add(r1);
            reviews.Add(r2);
        }

        public List<Category> CategoryGetAll()
        {
            return categories.ToList();
        }

        public List<Location> LocationGetByAddress(string address)
        {
            return locations.Where(l => l.Address.Contains(address)).ToList();
        }

        public Location LocationGetById(int id)
        {
            return locations.FirstOrDefault(l => l.LocationId == id);
        }

        public List<Review> ReviewsGetByLocationId(int locationId)
        {
            return reviews.Where(r => r.Location.LocationId == locationId).ToList();
        }

        public void AddReview(Review review)
        {
            reviews.Add(review);
        }
    }
}
