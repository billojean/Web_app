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
    public class Location
    {
        [Key]
        public string Username { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string T_title { get; set; }
        public int? UId { get; set; }
        [JsonIgnore]
        [ForeignKey("UId")]
        public virtual Users User { get; set; }
    }

}
