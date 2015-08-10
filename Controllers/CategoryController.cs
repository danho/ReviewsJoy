using ReviewsJoy.DAL;
using ReviewsJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewsJoy.Controllers
{
    public class CategoryController : Controller
    {
        private IDatabaseContext db;

        public CategoryController(IDatabaseContext db)
        {
            this.db = db;
        }

        public List<Category> CategoryGetAll()
        {
            return db.CategoryGetAll();
        }
    }
}