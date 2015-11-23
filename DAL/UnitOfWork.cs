using ReviewsJoy.DAL.Repository;
using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewsJoy.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDatabaseContext context;
        private GenericRepository<Review> reviewsRepository;
        private GenericRepository<Category> categoryRepository;
        private GenericRepository<Location> locationRepository;

        public UnitOfWork(IDatabaseContext context)
        {
            this.context = context;
        }

        public GenericRepository<Review> ReviewsRepository
        {
            get
            {
                if (reviewsRepository == null)
                {
                    this.reviewsRepository = new GenericRepository<Review>(context);
                }
                return this.reviewsRepository;
            }
        }

        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(context);
                }
                return this.categoryRepository;
            }
        }

        public GenericRepository<Location> LocationRepository
        {
            get
            {
                if (locationRepository == null)
                {
                    this.locationRepository = new GenericRepository<Location>(context);
                }
                return this.locationRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}