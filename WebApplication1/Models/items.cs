using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public  class items
    {
        [Key]
        public string item_id { get; set; }
        public string item_owner { get; set; }
        public string item_kind { get; set; }
    }
}
