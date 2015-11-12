using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewsJoy.DAL.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string Author { get; set; }
        public string ReviewText { get; set; }
        public string CategoryName { get; set; }
        public int Stars { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
    }
}