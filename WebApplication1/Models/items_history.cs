using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Items_history
    {
        public string Item_owner { get; set; }
        [Key]
        public string Item_id { get; set; }
        public string Item_kind { get; set; }
        public DateTime? Datetime_taken { get; set; }
        public DateTime? Datetime_return { get; set; }
    }
}