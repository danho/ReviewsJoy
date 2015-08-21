using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewsJoy.DAL
{
    public interface IDatabaseContext
    {
        List<Location> LocationGetByAddress(string address);
        Location LocationGetById(int id);
        List<Category> CategoryGetAll();
        List<Review> ReviewsGetByLocationId(int locationId, int? count);
        List<Review> ReviewsGeneralGetByLocationId(int locationId, int? count);
        List<Review> ReviewsCategorizedGetByLocationId(int locationId, int? count);
        void AddReview(Review review);
    }
}