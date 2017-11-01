using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Location_history
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string T_title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime? Datetime { get; set; }
    }
}