//using ReviewsJoy.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace ReviewsJoy.DAL.Repository
//{
//    public interface IReviewsRepository : IDisposable
//    {
//        IQueryable<Review> GetReviews();
//        IQueryable<Review> GetReviewById(int reviewId);
//        IQueryable<Review> GetMostRecentReviews(string placeId, int count);
//        IQueryable<Review> GetReviewsByCategory(int locationId, string category, int count);
//        void InsertReview(Review review);
//        void DeleteReview(int reviewId);
//        void UpdateReview(Review review);
//        void UpvoteReview(int reviewId);
//        void DownvoteReview(int reviewId);
//        void Save();
//    }
//}