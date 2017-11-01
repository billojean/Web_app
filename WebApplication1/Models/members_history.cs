using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Members_history
    {
        [Key]
        public int Id { get; set; }
        public string T_member { get; set; }
        public string T_pin { get; set; }
        public string T_title{ get; set; }
        public string T_identity { get; set; }
        public DateTime? Datetime_enter { get; set; }
        public DateTime? Datetime_leave { get; set; }
    }
}
