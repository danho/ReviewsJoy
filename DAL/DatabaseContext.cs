using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

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

        public int LocationIdGetByPlaceId(string placeId)
        {
            return Locations.Where(l => l.placeId == placeId)
                            .AsNoTracking()
                            .Select(n => n.LocationId)
                            .FirstOrDefault();
        }

        public Location LocationGetByPlaceId(string placeId)
        {
            return Locations.AsNoTracking()
                            .FirstOrDefault(l => l.placeId == placeId);
        }

        public Location LocationAdd(Location loc)
        {
            return Locations.Add(loc);
        }

        public List<Category> CategoryGetAll()
        {
            return Categories.AsNoTracking()
                             .ToList();
        }

        public Category CategoryGetByName(string name)
        {
            Category cat = null;
            if (!String.IsNullOrEmpty(name))
            {
                cat = Categories.AsNoTracking()
                                .FirstOrDefault(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            }
            return cat;
        }

        public Category CategoryAdd(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                return Categories.Add(new Category { Name = name.ToUpper() });
            }
            else
            {
                return null;
            }
        }

        public List<Review> ReviewsGetAll(string placeId)
        {
            return Reviews.Where(r => r.Location.placeId == placeId)
                          .AsNoTracking()
                          .ToList();
        }

        public List<Review> ReviewsGetByLocationId(int locationId, int? count)
        {
            if (count == null)
                return Reviews.Where(r => r.Location.LocationId == locationId)
                            .AsNoTracking()
                            .ToList();
            else
                return Reviews.Where(r => r.Location.LocationId == locationId)
                            .Take(count.Value)
                            .AsNoTracking()
                            .ToList();
        }

        public List<Review> ReviewsGeneralGetByLocationId(int locationId, int? count)
        {
            if (count == null)
                return Reviews.Where(r => r.Location.LocationId == locationId
                            && r.Category.Name.Equals("General", StringComparison.InvariantCultureIgnoreCase))
                                .AsNoTracking()
                                .ToList();
            else
                return Reviews.Where(r => r.Location.LocationId == locationId
                            && r.Category.Name.Equals("General", StringComparison.InvariantCultureIgnoreCase))
                                .Take(count.Value)
                                .AsNoTracking()
                                .ToList();
        }

        public List<Review> ReviewsCategorizedGetByLocationId(int locationId, int? count)
        {
            if (count == null)
                return Reviews.Where(r => r.Location.LocationId == locationId
                            && !r.Category.Name.Equals("General", StringComparison.InvariantCultureIgnoreCase))
                                .AsNoTracking()
                                .ToList();
            else
                return Reviews.Where(r => r.Location.LocationId == locationId
                            && !r.Category.Name.Equals("General", StringComparison.InvariantCultureIgnoreCase))
                                .Take(count.Value)
                                .AsNoTracking()
                                .ToList();
        }

        public List<Review> ReviewsGetByCategoryName(int locationId, string categoryName)
        {
            List<Review> catReviews = new List<Review>();
            if (!String.IsNullOrEmpty(categoryName))
            {
                catReviews = Reviews.Where(r => r.Category.Name.Equals(categoryName, StringComparison.InvariantCultureIgnoreCase))
                                    .AsNoTracking()
                                    .ToList();
            }
            return catReviews;
        }

        public Review AddReview(Review review)
        {
            var newReview = Reviews.Add(review);
            SaveChanges();
            return newReview;
        }

        public async Task<List<Review>> WarmUpDb()
        {
            return await Reviews.Take(100).ToListAsync();
        }
    }
}
