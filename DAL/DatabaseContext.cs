﻿using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReviewsJoy.DAL
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Location> Locations { get; set; }

        public List<Location> LocationGetByAddress(string address)
        {
            return Locations.Where(l => l.Address.Contains(address)).ToList();
        }

        public Location LocationGetById(int id)
        {
            return Locations.FirstOrDefault(l => l.LocationId == id);
        }
    }
}