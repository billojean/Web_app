using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Departments
    {
        public Departments()
        {
            users = new HashSet<users>();
        }
        [Key]
        public int DId { get; set; }
        public string Department { get; set; }
        public virtual ICollection<users> users { get; set; }
    }
}