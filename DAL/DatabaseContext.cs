using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using ReviewsJoy.DAL.DTO;

namespace ReviewsJoy.DAL
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public List<Location> LocationGetByAddress(string address)
        {
            if (String.IsNullOrEmpty(address))
                return new List<Location>();
            return Locations.Where(l => l.Address.Contains(address))
                            .ToList();
        }

        public Location LocationGetById(int id)
        {
            if (id == 0)
                return new Location();
            return Locations.FirstOrDefault(l => l.LocationId == id);
        }

        public int LocationIdGetByPlaceId(string placeId)
        {
            if (String.IsNullOrEmpty(placeId))
                return 0;
            return Locations.Where(l => l.placeId == placeId)
                            .AsNoTracking()
                            .Select(n => n.LocationId)
                            .FirstOrDefault();
        }

        public Location LocationGetByPlaceId(string placeId)
        {
            if (String.IsNullOrEmpty(placeId))
                return new Location();
            return Locations.AsNoTracking()
                            .FirstOrDefault(l => l.placeId == placeId);
        }

        public Location LocationAdd(Location loc)
        {
            if (loc == null)
                return new Location();
            return Locations.Add(loc);
        }

        public List<Category> CategoryGetAll()
        {
            return Categories.AsNoTracking()
                             .ToList();
        }

        public Category CategoryGetByName(string name)
        {
            if (String.IsNullOrEmpty(name))
                return new Category();
            return Categories.AsNoTracking()
                             .FirstOrDefault(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public Category CategoryAdd(string name)
        {
            if (String.IsNullOrEmpty(name))
                return new Category();
            return Categories.Add(new Category { Name = name.ToUpper() });
        }

        public List<Review> ReviewsGetAll(string placeId)
        {
            if (String.IsNullOrEmpty(placeId))
                return new List<Review>();
            return Reviews.Where(r => r.Location.placeId == placeId)
                          .AsNoTracking()
                          .ToList();
        }

        public List<Review> ReviewsGetByLocationId(int locationId, int? count)
        {
            if (locationId == 0)
                return new List<Review>();
            if (count == null)
                return Reviews.Where(r => r.Location.LocationId == locationId && r.IsActive == true)
                              .AsNoTracking()
                              .ToList();
            else
                return Reviews.Where(r => r.Location.LocationId == locationId && r.IsActive == true)
                              .Take(count.Value)
                              .AsNoTracking()
                              .ToList();
        }

        public List<Review> ReviewsGeneralGetByLocationId(int locationId, int? count)
        {
            if (locationId == 0)
                return new List<Review>();
            if (count == null)
                return Reviews.Where(r => r.Location.LocationId == locationId && r.Category.Name.Equals("General", StringComparison.InvariantCultureIgnoreCase)
                                        && r.IsActive == true)
                              .AsNoTracking()
                              .ToList();
            else
                return Reviews.Where(r => r.Location.LocationId == locationId && r.Category.Name.Equals("General", StringComparison.InvariantCultureIgnoreCase)
                                        && r.IsActive == true)
                              .Take(count.Value)
                              .AsNoTracking()
                              .ToList();
        }

        public List<Review> ReviewsCategorizedGetByLocationId(int locationId, int? count)
        {
            if (locationId == 0)
                return new List<Review>();
            if (count == null)
                return Reviews.Where(r => r.Location.LocationId == locationId && !r.Category.Name.Equals("General", StringComparison.InvariantCultureIgnoreCase)
                                        && r.IsActive == true)
                              .AsNoTracking()
                              .ToList();
            else
                return Reviews.Where(r => r.Location.LocationId == locationId && !r.Category.Name.Equals("General", StringComparison.InvariantCultureIgnoreCase)
                                        && r.IsActive == true)
                              .Take(count.Value)
                              .AsNoTracking()
                              .ToList();
        }

        public List<Review> ReviewsGetByCategoryName(int locationId, string categoryName)
        {
            if (locationId == 0 || String.IsNullOrEmpty(categoryName))
                return new List<Review>();
            return Reviews.Where(r => r.Category.Name.Equals(categoryName, StringComparison.InvariantCultureIgnoreCase)
                                            && r.IsActive == true)
                                       .AsNoTracking()
                                       .ToList();
        }

        public List<Review> ReviewsGetLatest(int count)
        {
            if (count <= 0)
                return new List<Review>();
            return Reviews.OrderByDescending(r => r.ReviewId)
                            .Where(r => r.IsActive == true)
                            .Take(count)
                            .AsNoTracking()
                            .ToList();
        }

        public Review AddReview(Review review)
        {
            if (review == null)
                return new Review();
            var newReview = Reviews.Add(review);
            SaveChanges();
            return newReview;
        }

        public List<ReviewDTO> ReviewsGetMostRecent(string placeId, int count)
        {
            if (String.IsNullOrEmpty(placeId) || count == 0)
                return new List<ReviewDTO>();
            return Reviews.Where(r => r.Location.placeId == placeId && r.IsActive == true)
                          .OrderByDescending(r => r.UpVotes - r.DownVotes)
                          .ThenByDescending(r => r.ReviewId)
                          .AsNoTracking()
                          .Select(r => new ReviewDTO
                          {
                              Id = r.ReviewId,
                              Author = r.Author,
                              CategoryName = r.Category.Name,
                              LocationId = r.Location.LocationId,
                              ReviewText = r.ReviewText,
                              Stars = r.Stars,
                              UpVotes = r.UpVotes,
                              DownVotes = r.DownVotes
                          })
                          .Take(count)
                          .ToList();
        }

        public List<ReviewDTO> ReviewsFilterByCategory(int locationId, string category, int count)
        {
            if (locationId == 0)
                return new List<ReviewDTO>();

            category = String.IsNullOrEmpty(category) ? String.Empty : category.Trim();

            return Reviews.Where(r => r.Location.LocationId == locationId && r.Category.Name.Contains(category)
                                    && r.IsActive == true)
                          .OrderByDescending(r => r.UpVotes - r.DownVotes)
                          .ThenByDescending(r => r.ReviewId)
                          .AsNoTracking()
                          .Select(r => new ReviewDTO
                          {
                              Id = r.ReviewId,
                              Author = r.Author,
                              CategoryName = r.Category.Name,
                              LocationId = r.Location.LocationId,
                              ReviewText = r.ReviewText,
                              Stars = r.Stars,
                              UpVotes = r.UpVotes,
                              DownVotes = r.DownVotes
                          })
                          .Take(count)
                          .ToList();
        }

        public ReviewDTO UpVote(int reviewId)
        {
            if (reviewId == 0)
                return null;

            var r = Reviews.FirstOrDefault(x => x.ReviewId == reviewId);

            if (r == null)
                return null;

            r.UpVotes++;
            SaveChanges();
            return new ReviewDTO
            {
                Id = r.ReviewId,
                Author = r.Author,
                CategoryName = r.Category.Name,
                LocationId = r.Location.LocationId,
                ReviewText = r.ReviewText,
                Stars = r.Stars,
                UpVotes = r.UpVotes,
                DownVotes = r.DownVotes
            };
        }
        public ReviewDTO DownVote(int reviewId)
        {
            if (reviewId == 0)
                return null;

            var r = Reviews.FirstOrDefault(x => x.ReviewId == reviewId);

            if (r == null)
                return null;

            r.DownVotes++;
            SaveChanges();
            return new ReviewDTO
            {
                Id = r.ReviewId,
                Author = r.Author,
                CategoryName = r.Category.Name,
                LocationId = r.Location.LocationId,
                ReviewText = r.ReviewText,
                Stars = r.Stars,
                UpVotes = r.UpVotes,
                DownVotes = r.DownVotes
            };
        }
    }
}
