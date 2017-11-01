using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public  class Items
    {
        [Key]
        public string Item_id { get; set; }
        public string Item_owner { get; set; }
        public string Item_kind { get; set; }
    }
}
