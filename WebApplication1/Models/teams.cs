using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Teams
    {
        public string Title { get; set; }
        [Key]
        public string Pin { get; set; }
        public string Creator { get; set; }
        public string Visibility { get; set; }
    }
}