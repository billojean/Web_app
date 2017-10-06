using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class users
    {
        public users()
        {
           
        }
        [Key]
        public int Id { get; set; }        
        public string FirstName { get; set; }        
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        public string Email { get; set; }
        public string OfficePhone { get; set; }
        public string MobilePhone { get; set; }
        public byte[] Pic { get; set; }
        public int? DId { get; set; }
        [JsonIgnore]
        public virtual ICollection<location> location { get; set; }
        [JsonIgnore]
        public virtual Departments Departments { get; set; }       
    }
}