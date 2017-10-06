using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class location_history
    {
        [Key]
        public int Id { get; set; }
        public string username { get; set; }
        public string t_title { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime? datetime { get; set; }
    }
}