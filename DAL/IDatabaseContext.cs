using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using ReviewsJoy.DAL.DTO;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ReviewsJoy.DAL
{
    public interface IDatabaseContext
    {
        DbSet<Location> Locations { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        List<Location> LocationGetByAddress(string address);
        Location LocationGetById(int id);
        int LocationIdGetByPlaceId(string id);
        Location LocationGetByPlaceId(string placeId);
        Location LocationAdd(Location location);
        List<Category> CategoryGetAll();
        Category CategoryGetByName(string name);
        Category CategoryAdd(string name);
        List<Review> ReviewsGetAll(string placeId);
        List<Review> ReviewsGeneralGetByLocationId(int locationId, int? count);
        List<Review> ReviewsCategorizedGetByLocationId(int locationId, int? count);
        List<Review> ReviewsGetByCategoryName(int locationId, string categoryName);
        List<ReviewDTO> ReviewsGetMostRecent(string placeId, int count);
        Review AddReview(Review review);
        List<Review> ReviewsGetLatest(int count);
        List<ReviewDTO> ReviewsFilterByCategory(int locationId, string category, int count);
        ReviewDTO UpVote(int reviewId);
        ReviewDTO DownVote(int reviewId);
        void Save();
        void Dispose();
    }
}