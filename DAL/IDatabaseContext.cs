using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using ReviewsJoy.DAL.DTO;

namespace ReviewsJoy.DAL
{
    public interface IDatabaseContext
    {
        List<Location> LocationGetByAddress(string address);
        Location LocationGetById(int id);
        int LocationIdGetByPlaceId(string id);
        Location LocationGetByPlaceId(string placeId);
        Location LocationAdd(Location location);
        List<Category> CategoryGetAll();
        Category CategoryGetByName(string name);
        Category CategoryAdd(string name);
        List<Review> ReviewsGetAll(string placeId);
        List<Review> ReviewsGetByLocationId(int locationId, int? count);
        List<Review> ReviewsGeneralGetByLocationId(int locationId, int? count);
        List<Review> ReviewsCategorizedGetByLocationId(int locationId, int? count);
        List<Review> ReviewsGetByCategoryName(int locationId, string categoryName);
        List<ReviewDTO> ReviewsGetMostRecent(string placeId, int count);
        Review AddReview(Review review);
        List<Review> ReviewsGetLatest(int count);
        Task<List<Review>> WarmUpDb();
    }
}