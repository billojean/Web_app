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
        public DbSet<users> Users { get; set; }
        public DbSet<teams> Teams { get; set; }
        public DbSet<items> Items { get; set; }
        public DbSet<location> Locations { get; set; }
        public DbSet<location_history> Location_history { get; set; }
        public DbSet<Laptops> laptops { get; set; }
        public DbSet<Vehicles> vehicles { get; set; }
        public DbSet<SpareParts> spareparts { get; set; }
        public DbSet<t_members> T_members { get; set; }
        public DbSet<members_history> members_history { get; set; }
        public DbSet<items_history> items_history { get; set; }
        public DbSet<Departments> departments { get; set; }

        public IQueryable<items> FindItem(string user)
        { return Items.Where(u => u.item_owner == user); }

        public bool titleExists(teams team)
        { return Teams.Count(e => e.title == team.title) > 0; }

        public bool InTeam(teams team)
        {
            return T_members.Count(u => u.t_member == team.creator) > 0;
        }

        public t_members FindMember(string user)
        { return T_members.SingleOrDefault(u => u.t_member == user); }

        public users FindUserByEmail(string Email)
        { return Users.SingleOrDefault(u => u.Email == Email); }


        public IQueryable<location> FindTeamMembers(string user, string title)
        {
            return Locations.Where(u => u.username != user && u.t_title == title).OrderBy(u=>u.username);

        }

        public t_members FindUserOnLocations(location location)
        { return T_members.SingleOrDefault(u => u.t_member == location.username); }
        public users FindUserId(location location)
        { return Users.SingleOrDefault(u => u.UserName == location.username); }

        public teams FindInvTeams(string pin)
        { return Teams.SingleOrDefault(u => u.visibility == "false" && u.pin == pin); }

        public IQueryable<t_members> FindTmember(string user)
        {

            return T_members.Where(u => u.t_member == user);

        }
 
    }
    
}
