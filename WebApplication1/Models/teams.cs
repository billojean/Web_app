using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class teams
    {
        public string title { get; set; }
        [Key]
        public string pin { get; set; }
        public string creator { get; set; }
        public string visibility { get; set; }
    }
}