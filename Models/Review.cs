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
        [Required]
        [MaxLength(500)]
        public string ReviewText { get; set; }
        [Required]
        [MaxLength(50)]
        public string Author { get; set; }
        [Required]
        [Range(0, 5)]
        public int Stars { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public bool IsActive { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}