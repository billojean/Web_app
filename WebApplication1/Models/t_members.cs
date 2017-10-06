using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class t_members
    {
        [Key]
        public string t_member { get; set; }
        public string t_pin { get; set; }
        public string t_title { get; set; }
        public string t_identity { get; set; }
    }
}
