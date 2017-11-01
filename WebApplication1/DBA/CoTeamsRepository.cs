using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.DBA
{
    public class CoTeamsRepository:DataContext
    {
        public IQueryable<Teams> GetInvisibleTeams()
        { return Teams.Where(u => u.Visibility == "true"); }

        public IQueryable<Teams> GetAllTeams()
        { return Teams.OrderBy(a => a.Title); }

        public IQueryable<Items> FindItem(string user)
        { return Items.Where(u => u.Item_owner == user); }

        public bool TitleExists(Teams team)
        { return Teams.Count(e => e.Title == team.Title) > 0; }

 
        public bool IsCreator(string creator)
        {
            return Teams.Count(u => u.Creator == creator) > 0;
        }
        public T_members FindMember(string user)
        { return T_members.SingleOrDefault(u => u.T_member == user); }

        public Users FindUserByEmail(string Email)
        { return Users.SingleOrDefault(u => u.Email == Email); }


        public IQueryable<Location> FindTeamMembers(string user, string title)
        {
            return Locations.Where(u => u.Username != user && u.T_title == title).OrderBy(u => u.Username);
        }

        public T_members FindUserOnLocations(Location location)
        { return T_members.SingleOrDefault(u => u.T_member == location.Username); }

        public Users FindUserId(Location location)
        { return Users.SingleOrDefault(u => u.UserName == location.Username); }

        public Teams FindInvisibleTeams(string pin)
        { return Teams.SingleOrDefault(u => u.Visibility == "false" && u.Pin == pin); }

        public IQueryable<T_members> FindTmember(string user)
        {
            return T_members.Where(u => u.T_member == user);
        }
        public void DeleteMember(string user,T_members t_member)
        {
            T_members.Remove(t_member);

            Members_history result = (from p in Members_history
                                      where p.T_member == user && p.Datetime_leave == null
                                      select p).SingleOrDefault();

            result.Datetime_leave = DateTime.Now;
        }
        public void DeleteAssociateMembers(string user)
        {
            var query = Teams.SingleOrDefault(u => u.Creator == user);
            var pin =  query.Pin;
            Teams.RemoveRange(Teams.Where(u => u.Pin == pin));
            T_members.RemoveRange(T_members.Where(u => u.T_pin == pin));

            (from p in Members_history
             where p.T_pin == pin && p.Datetime_leave == null
             select p).ToList().ForEach(x => x.Datetime_leave = DateTime.Now);
        }
        public void InsertMember(Teams team)
        {
            T_members t_members = new T_members
            {
                T_member = team.Creator,
                T_title = team.Title,
                T_pin = team.Pin,
                T_identity = "creator"

            };
            Members_history member_history = new Members_history
            {
                T_member = team.Creator,
                T_title = team.Title,
                T_pin = team.Pin,
                T_identity = "creator",
                Datetime_enter = DateTime.Now
            };

            T_members.Add(t_members);
            Members_history.Add(member_history);
        }
        public void InsertItemHistory(Items item)
        {
            Items_history item_history = new Items_history
            {
                Item_owner = item.Item_owner,
                Item_id = item.Item_id,
                Item_kind = item.Item_kind,
                Datetime_taken = DateTime.Now,
                Datetime_return = null
            };
            if (ItemExistsInHistory(item.Item_id))
            {
                Items_history.Attach(item_history);
                var entry = Entry(item_history);
                entry.Property(e => e.Datetime_taken).IsModified = true;
                entry.Property(e => e.Item_kind).IsModified = true;
                entry.Property(e => e.Item_owner).IsModified = true;
                entry.Property(e => e.Datetime_return).IsModified = true;
            }
            else
            {
                Items_history.Add(item_history);

            }
        }
        public IQueryable<Items> GetUserItems(string user)
        {
            return Items.Where(u => u.Item_owner == user);
        }
        public void ReturnInItemHistory(Items item)
        {
            Items_history item_history = new Items_history
            {

                Item_owner = item.Item_owner,
                Item_id = item.Item_id,
                Item_kind = item.Item_kind,
                Datetime_return = DateTime.Now
            };
            Items_history.Attach(item_history);
            var entry = Entry(item_history);
            entry.Property(e => e.Datetime_return).IsModified = true;
        }

        public IQueryable<Location> GetTeamLocations(string title)
        {
            return Locations.Where(a => a.T_title.Equals(title));
        }
        public void AddLocation(Location location)
        {
            var query = FindUserOnLocations(location);
            var query2 = FindUserId(location);
            if (query == null)
            {
                location.T_title = "";
            }
            else
            {
                string title = query.T_title;

                location.T_title = title.Trim();
            }
            location.UId = query2.Id;
            Locations.Add(location);


            try
            {
                SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LocationExists(location.Username))
                {
                    Locations.Attach(location);
                    var entry = Entry(location);
                    entry.Property(e => e.UId).IsModified = true;
                    entry.Property(e => e.T_title).IsModified = true;
                    entry.Property(e => e.Latitude).IsModified = true;
                    entry.Property(e => e.Longitude).IsModified = true;
                }
                else
                {
                    throw;
                }
            }
            Location_history location_history = new Location_history
            {
                Username = location.Username,
                T_title = location.T_title,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Datetime = DateTime.Now
            };
            Location_history.Add(location_history);
            SaveChanges();
        }
        public void AddMemberHistory(T_members t_members)
        {
            Members_history member_history = new Members_history
            {
                T_member = t_members.T_member,
                T_title = t_members.T_title,
                T_pin = t_members.T_pin,
                T_identity = "member",
                Datetime_enter = DateTime.Now
            };

            Members_history.Add(member_history);
        }
        public IQueryable GetTeamMembersInfo(IQueryable<T_members> member)
        {
            string pin = member.First().T_pin;
            var join = from t1 in T_members
                       join t2 in Users on t1.T_member equals t2.UserName
                       where t1.T_pin == pin
                       join t3 in Departments on t2.DId equals t3.DId
                       orderby t1.T_identity
                       select new { t2.FirstName, t2.LastName, t1.T_title, t1.T_member, t1.T_identity, t2.Email, t2.OfficePhone, t2.MobilePhone, t3.Department, t2.Pic };
            return join;
        }
        public IQueryable<Location_history> GetPreviousLocations(string title, double time) 
        {
            DateTime datetime1 = DateTime.Now.AddMinutes(-time);
            DateTime datetime2 = DateTime.Now;
            return Location_history.Where(a => a.T_title.Equals(title) && a.Datetime >= datetime1 && a.Datetime <= datetime2);
        }
        private bool ItemExistsInHistory(string id)
        {
            return Items_history.Count(e => e.Item_id == id) > 0;
        }
        private bool LocationExists(string id)
        {
            return Locations.Count(e => e.Username == id) > 0;
        }
    }
}