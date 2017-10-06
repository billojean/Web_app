using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class location
    {
        [Key]
        public string username { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string t_title { get; set; }
        public int? UId { get; set; }
        [JsonIgnore]
        [ForeignKey("UId")]
        public virtual users users { get; set; }
    }

}
