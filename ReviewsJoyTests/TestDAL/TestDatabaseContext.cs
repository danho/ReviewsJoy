using ReviewsJoy.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewsJoy.Models;
using Moq;

namespace ReviewsJoyTests.TestDAL
{
    public class TestDatabaseContext /*: IDatabaseContext*/
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
                YCoordinate = 30.1,
                placeId = "a"
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
                YCoordinate = 32.1,
                placeId = "b"
            };

            locations = new List<Location>();
            locations.Add(l1);
            locations.Add(l2);

            var c1 = new Category
            {
                CategoryId = 1,
                Name = "General"
            };

            var c2 = new Category
            {
                CategoryId = 2,
                Name = "Food"
            };
            var c3 = new Category
            {
                CategoryId = 3,
                Name = "Parking"
            };

            categories = new List<Category>();
            categories.Add(c1);
            categories.Add(c2);
            categories.Add(c3);

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
            var r3 = new Review
            {
                ReviewId = 3,
                Author = "B",
                Category = c2,
                Location = l2,
                ReviewText = "Plenty of parking spaces",
                Stars = 10
            };
            var r4 = new Review
            {
                ReviewId = 4,
                Author = "B",
                Category = c1,
                Location = l2,
                ReviewText = "Plenty of parking spaces",
                Stars = 10
            };

            reviews = new List<Review>();
            reviews.Add(r1);
            reviews.Add(r2);
            reviews.Add(r3);
            reviews.Add(r4);
        }

        public IDatabaseContext GetMockDatabase()
        {
            var mock = new Mock<IDatabaseContext>();

            mock.Setup(m => m.CategoryGetAll()).Returns(() => categories.ToList());
            mock.Setup(m => m.LocationGetByAddress(It.IsAny<string>()))
                .Returns((string s) => locations.Where(l => l.Address.Contains(s)).ToList());
            mock.Setup(m => m.LocationGetById(It.IsAny<int>()))
                .Returns((int i) => locations.FirstOrDefault(l => l.LocationId == i));
            mock.Setup(m => m.ReviewsGetByLocationId(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns((int a, int? b) =>
                    {
                        if (b == null)
                        {
                            return reviews
                                .Where(r => r.Location.LocationId == a)
                                .ToList();
                        }
                        else
                        {
                            return reviews
                                .Where(r => r.Location.LocationId == a)
                                .Take(b.Value)
                                .ToList();
                        }
                    }
                );
            mock.Setup(m => m.ReviewsGeneralGetByLocationId(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns((int a, int? b) =>
                    {
                        if (b == null)
                            return reviews.Where(r => r.Location.LocationId == a
                                    && r.Category.Name.Equals("general", StringComparison.InvariantCultureIgnoreCase))
                                        .ToList();
                        else
                            return reviews.Where(r => r.Location.LocationId == a
                                        && r.Category.Name.Equals("general", StringComparison.InvariantCultureIgnoreCase))
                                            .Take(b.Value)
                                                .ToList();
                    }
                );
            mock.Setup(m => m.ReviewsCategorizedGetByLocationId(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns((int locationId, int? count) =>
                    {
                        if (count == null)
                            return reviews.Where(r => r.Location.LocationId == locationId
                                    && !r.Category.Name.Equals("general", StringComparison.InvariantCultureIgnoreCase))
                                        .ToList();
                        else
                            return reviews.Where(r => r.Location.LocationId == locationId
                                        && !r.Category.Name.Equals("general", StringComparison.InvariantCultureIgnoreCase))
                                            .Take(count.Value)
                                                .ToList();
                    }
                 );
            mock.Setup(m => m.CategoryGetByName(It.IsAny<string>()))
                .Returns((string name) =>
                {
                    Category cat = null;
                    if (!String.IsNullOrEmpty(name))
                    {
                        name = name.Trim().ToUpper();
                        cat = categories.FirstOrDefault(c => c.Name.ToUpper() == name);
                    }
                    return cat;
                }
            );
            mock.Setup(m => m.ReviewsGetByCategoryName(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((int locationId, string categoryName) =>
                {
                    List<Review> catReviews = new List<Review>();
                    if (!String.IsNullOrEmpty(categoryName))
                    {
                        categoryName = categoryName.Trim().ToUpper();
                        var category = categories.FirstOrDefault(c => c.Name.ToUpper() == categoryName);
                        if (category != null)
                        {
                            reviews = reviews.Where(r => r.Category.CategoryId == category.CategoryId)
                                        .ToList();
                        }
                    }
                    return catReviews;
                }
            );
            mock.Setup(m => m.CategoryAdd(It.IsAny<string>()))
                .Returns((string name) =>
                {
                    var id = categories.Last().CategoryId + 1;
                    var newCat = new Category { CategoryId = id, Name = name };
                    categories.Add(newCat);
                    return newCat;
                }
            );
            mock.Setup(m => m.LocationGetByPlaceId(It.IsAny<string>()))
                .Returns((string placeId) =>
                {
                    return locations.FirstOrDefault(l => l.placeId == placeId);
                }
            );
            mock.Setup(m => m.LocationAdd(It.IsAny<Location>()))
                .Returns((Location location) =>
                {
                    location.LocationId = locations.Last().LocationId + 1;
                    locations.Add(location);
                    return location;
                }
            );
            mock.Setup(m => m.AddReview(It.IsAny<Review>()))
                .Returns((Review review) =>
                {
                    review.ReviewId = reviews.Last().ReviewId + 1;
                    reviews.Add(review);
                    return review;
                }
            );
            mock.Setup(m => m.LocationIdGetByPlaceId(It.IsAny<string>()))
                .Returns((string s) =>
                {
                    return locations.Where(l => l.placeId == s).Select(m => m.LocationId).FirstOrDefault();
                }
            );
            mock.Setup(m => m.ReviewsGetAll(It.IsAny<string>()))
                .Returns((string placeId) =>
                {
                    return reviews.Where(r => r.Location.placeId == placeId).ToList();
                }
            );

            return mock.Object;
        }

        //public List<Category> CategoryGetAll()
        //{
        //    return categories.ToList();
        //}

        //public List<Location> LocationGetByAddress(string address)
        //{
        //    return locations.Where(l => l.Address.Contains(address)).ToList();
        //}

        //public Location LocationGetById(int id)
        //{
        //    return locations.FirstOrDefault(l => l.LocationId == id);
        //}

        //public List<Review> ReviewsGetByLocationId(int locationId, int? count)
        //{
        //    if (count == null)
        //        return reviews.Where(r => r.Location.LocationId == locationId)
        //                    .ToList();
        //    else
        //        return reviews.Where(r => r.Location.LocationId == locationId)
        //                    .Take(count.Value)
        //                        .ToList();
        //}

        //public List<Review> ReviewsGeneralGetByLocationId(int locationId, int? count)
        //{
        //    if (count == null)
        //        return reviews.Where(r => r.Location.LocationId == locationId
        //                && r.Category.Name.Equals("general", StringComparison.InvariantCultureIgnoreCase))
        //                    .ToList();
        //    else
        //        return reviews.Where(r => r.Location.LocationId == locationId
        //                    && r.Category.Name.Equals("general", StringComparison.InvariantCultureIgnoreCase))
        //                        .Take(count.Value)
        //                            .ToList();
        //}

        //public List<Review> ReviewsCategorizedGetByLocationId(int locationId, int? count)
        //{
        //    if (count == null)
        //        return reviews.Where(r => r.Location.LocationId == locationId
        //                && !r.Category.Name.Equals("general", StringComparison.InvariantCultureIgnoreCase))
        //                    .ToList();
        //    else
        //        return reviews.Where(r => r.Location.LocationId == locationId
        //                    && !r.Category.Name.Equals("general", StringComparison.InvariantCultureIgnoreCase))
        //                        .Take(count.Value)
        //                            .ToList();
        //}

        //public void AddReview(Review review)
        //{
        //    reviews.Add(review);
        //}

        //public Category CategoryGetByName(string name)
        //{
        //    Category cat = null;
        //    if (!String.IsNullOrEmpty(name))
        //    {
        //        name = name.Trim().ToUpper();
        //        cat = categories.FirstOrDefault(c => c.Name.ToUpper() == name);
        //    }
        //    return cat;
        //}

        //public List<Review> ReviewsGetByCategoryName(int locationId, string categoryName)
        //{
        //    List<Review> catReviews = new List<Review>();
        //    if (!String.IsNullOrEmpty(categoryName))
        //    {
        //        var category = CategoryGetByName(categoryName);
        //        if (category != null)
        //        {
        //            reviews = reviews.Where(r => r.Category.CategoryId == category.CategoryId)
        //                        .ToList();
        //        }
        //    }
        //    return catReviews;
        //}

        //public Category CategoryAdd(string name)
        //{
        //    var id = categories.Last().CategoryId + 1;
        //    var newCat = new Category { CategoryId = id, Name = name };
        //    categories.Add(newCat);
        //    return newCat;
        //}

        //public Location LocationGetByPlaceId(string placeId)
        //{
        //    return locations.FirstOrDefault(l => l.placeId == placeId);
        //}

        //public Location LocationAdd(Location location)
        //{
        //    location.LocationId = locations.Last().LocationId + 1;
        //    locations.Add(location);
        //    return location;
        //}
    }
}
