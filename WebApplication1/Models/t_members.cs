using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class T_members
    {
        [Key]
        public string T_member { get; set; }
        public string T_pin { get; set; }
        public string T_title { get; set; }
        public string T_identity { get; set; }
    }
}
