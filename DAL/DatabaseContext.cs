using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReviewsJoy.DAL
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public List<Location> LocationGetByAddress(string address)
        {
            return Locations.Where(l => l.Address.Contains(address)).ToList();
        }

        public Location LocationGetById(int id)
        {
            return Locations.FirstOrDefault(l => l.LocationId == id);
        }

        public Location LocationGetByPlaceId(string placeId)
        {
            return Locations.FirstOrDefault(l => l.placeId == placeId);
        }

        public Location LocationAdd(Location loc)
        {
            return Locations.Add(loc);
        }

        public List<Category> CategoryGetAll()
        {
            return Categories.ToList();
        }

        public Category CategoryGetByName(string name)
        {
            Category cat = null;
            if (!String.IsNullOrEmpty(name))
            {
                name = name.Trim().ToUpper();
                cat = Categories.FirstOrDefault(c => c.Name.ToUpper() == name);
            }
            return cat;
        }

        public Category CategoryAdd(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                return Categories.Add(new Category { Name = name });
            }
            else
            {
                return null;
            }
        }

        public List<Review> ReviewsGetByLocationId(int locationId, int? count)
        {
            if (count == null)
                return Reviews.Where(r => r.Location.LocationId == locationId)
                            .ToList();
            else
                return Reviews.Where(r => r.Location.LocationId == locationId)
                            .Take(count.Value)
                                .ToList();
        }

        public List<Review> ReviewsGeneralGetByLocationId(int locationId, int? count)
        {
            if (count == null)
                return Reviews.Where(r => r.Location.LocationId == locationId
                            && r.Category.Name.Equals("general", StringComparison.InvariantCultureIgnoreCase))
                                .ToList();
            else
                return Reviews.Where(r => r.Location.LocationId == locationId
                            && r.Category.Name.Equals("general", StringComparison.InvariantCultureIgnoreCase))
                                .Take(count.Value)
                                    .ToList();
        }

        public List<Review> ReviewsCategorizedGetByLocationId(int locationId, int? count)
        {
            if (count == null)
                return Reviews.Where(r => r.Location.LocationId == locationId
                            && !r.Category.Name.Equals("general", StringComparison.InvariantCultureIgnoreCase))
                                .ToList();
            else
                return Reviews.Where(r => r.Location.LocationId == locationId
                            && !r.Category.Name.Equals("general", StringComparison.InvariantCultureIgnoreCase))
                                .Take(count.Value)
                                    .ToList();
        }

        public List<Review> ReviewsGetByCategoryName(int locationId, string categoryName)
        {
            List<Review> catReviews = new List<Review>();
            if (!String.IsNullOrEmpty(categoryName))
            {
                var category = CategoryGetByName(categoryName);
                if (category != null)
                {
                    catReviews = Reviews.Where(r => r.Category.CategoryId == category.CategoryId
                                    && r.Location.LocationId == locationId)
                                        .ToList();
                }
            }
            return catReviews;
        }

        public void AddReview(Review review)
        {
            Reviews.Add(review);
            SaveChanges();
        }
    }
}