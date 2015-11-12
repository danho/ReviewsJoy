using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReviewsJoy.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public virtual Location Location { get; set; }
        public virtual Category Category { get; set; }
        public string ReviewText { get; set; }
        public string Author { get; set; }
        [Range(0, 10)]
        public int Stars { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
    }
}