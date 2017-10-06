using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class items_history
    {
        public string item_owner { get; set; }
        [Key]
        public string item_id { get; set; }
        public string item_kind { get; set; }
        public DateTime? datetime_taken { get; set; }
        public DateTime? datetime_return { get; set; }
    }
}