using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.DBA
{
    public class DataContext:DbContext
    {
        public DataContext()

            :base("Connection")
             {

             }

        public DbSet<Users> Users { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Location_history> Location_history { get; set; }
        public DbSet<Laptops> Laptops { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<SpareParts> Spareparts { get; set; }
        public DbSet<T_members> T_members { get; set; }
        public DbSet<Members_history> Members_history { get; set; }
        public DbSet<Items_history> Items_history { get; set; }
        public DbSet<Departments> Departments { get; set; }
        
    }
    
}
