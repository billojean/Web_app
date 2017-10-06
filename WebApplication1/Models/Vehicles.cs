using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Vehicles
    {
        [Key]
        public string Id { get; set; }
        public string Kind { get; set; }
    }
}
