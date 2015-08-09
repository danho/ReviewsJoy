namespace ReviewsJoy.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ReviewsJoy.DAL.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ReviewsJoy.DAL.DatabaseContext context)
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

            context.Locations.AddOrUpdate(l => l.LocationId,
                l1,
                l2);

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

            context.Categories.AddOrUpdate(c => c.CategoryId,
                c1,
                c2);

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

            context.Reviews.AddOrUpdate(r => r.ReviewId,
                r1,
                r2);
        }
    }
}
