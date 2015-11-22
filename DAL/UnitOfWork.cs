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
        private DatabaseContext context;
        private GenericRepository<Review> reviewsRepository;

        public UnitOfWork(DatabaseContext context)
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