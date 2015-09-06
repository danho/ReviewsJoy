using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReviewsJoy.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }
        public string placeId { get; set; }
    }
}