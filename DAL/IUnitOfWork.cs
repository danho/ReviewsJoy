﻿using ReviewsJoy.DAL.Repository;
using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewsJoy.DAL
{
    public interface IUnitOfWork
    {
        GenericRepository<Review> ReviewsRepository { get; }
        GenericRepository<Category> CategoryRepository { get; }
        GenericRepository<Location> LocationRepository { get; }
        void Save();
        void Dispose();
    }
}