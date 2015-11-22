//using ReviewsJoy.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;


//namespace ReviewsJoy.DAL.Repository
//{
//    public class ReviewsRepository : IReviewsRepository
//    {
//        private DatabaseContext db;

//        public ReviewsRepository(DatabaseContext db)
//        {
//            this.db = db;
//        }

//        public IQueryable<Review> GetReviews()
//        {
//            return db.Reviews;
//        }

//        public IQueryable<Review> GetReviewById(int reviewId)
//        {
//            return db.Reviews.Where(r => r.ReviewId == reviewId);
//        }

//        public IQueryable<Review> GetMostRecentReviews(string placeId, int count)
//        {
//            return db.Reviews.Where(r => r.Location.placeId == placeId && r.IsActive == true)
//                                .OrderByDescending(r => r.UpVotes - r.DownVotes)
//                                          .ThenByDescending(r => r.ReviewId);
//        }

//        public IQueryable<Review> GetReviewsByCategory(int locationId, string category, int count)
//        {
//            return db.Reviews.Where(r => r.Location.LocationId == locationId && r.Category.Name.Contains(category) && r.IsActive == true)
//                                .OrderByDescending(r => r.UpVotes - r.DownVotes)
//                                    .ThenByDescending(r => r.ReviewId)
//                                        .Take(count);
//        }

//        public void InsertReview(Review review)
//        {
//            db.Reviews.Add(review);
//        }

//        public void DeleteReview(int reviewId)
//        {
//            var review = db.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);
//            db.Reviews.Remove(review);
//        }

//        public void UpdateReview(Review review)
//        {
//            db.Entry(review).State = EntityState.Modified;
//        }

//        public void UpvoteReview(int reviewId)
//        {
//            var _review = db.Reviews.Where(r => r.ReviewId == reviewId).FirstOrDefault();
//            if (_review != null)
//            {
//                _review.UpVotes++;
//                UpdateReview(_review);
//            }
//            Save();
//            //db.Entry(_review).State = EntityState.Detached;
//        }

//        public void DownvoteReview(int reviewId)
//        {
//            var _review = db.Reviews.Where(r => r.ReviewId == reviewId).FirstOrDefault();
//            if (_review != null)
//            {
//                _review.DownVotes++;
//                UpdateReview(_review);
//            }
//            Save();
//        }

//        public void Save()
//        {
//            db.SaveChanges();
//        }

//        private bool disposed = false;

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!this.disposed)
//            {
//                if (disposing)
//                {
//                    db.Dispose();
//                }
//            }
//            this.disposed = true;
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//    }
//}