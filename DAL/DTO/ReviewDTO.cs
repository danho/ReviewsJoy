using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewsJoy.DAL.DTO
{
    public class ReviewDTO
    {
        public int LocationId { get; set; }
        public string Author { get; set; }
        public string ReviewText { get; set; }
        public string CategoryName { get; set; }
        public int Stars { get; set; }
    }
}