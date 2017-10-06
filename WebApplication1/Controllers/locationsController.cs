using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.DBA;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class locationsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/locations
        [ResponseType(typeof(location))]
        public IHttpActionResult Getlocations(string user)
        {
            var query = db.FindMember(user);
            if (query == null)
            {
                return NotFound();
            }

            string title = query.t_title;

            var getTeammembers = db.FindTeamMembers(user, title);

            if (getTeammembers.Count() == 0)
            {
                return NotFound();
            }
            return Ok(getTeammembers);
        }

        public IQueryable <location> GetTeamMembersLocation(string title)
        {           
            return db.Locations.Where(a => a.t_title.Equals(title));
        }

        // GET: api/locations/5
        [ResponseType(typeof(location))]
        public IHttpActionResult Getlocation(string id)
        {
            location location = db.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/locations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlocation(string id, location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.username)
            {
                return BadRequest();
            }

            db.Entry(location).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!locationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/locations
        [ResponseType(typeof(location))]
        public IHttpActionResult Postlocation(location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var query = db.FindUserOnLocations(location);
            var query2 = db.FindUserId(location);
            if (query == null)
            {
                location.t_title = "";
            }
            else
            {
                string title = query.t_title;

                location.t_title = title.Trim();
            }
            location.UId = query2.Id;
            db.Locations.Add(location);


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (locationExists(location.username))
                {
                    db.Locations.Attach(location);
                    var entry = db.Entry(location);
                    entry.Property(e => e.UId).IsModified = true;
                    entry.Property(e => e.t_title).IsModified = true;
                    entry.Property(e => e.latitude).IsModified = true;
                    entry.Property(e => e.longitude).IsModified = true;
                }
                else
                {
                    throw;
                }
            }
            location_history location_history = new location_history
            {
                username = location.username,
                t_title = location.t_title,
                latitude = location.latitude,
                longitude = location.longitude,
                datetime = DateTime.Now
            };
            db.Location_history.Add(location_history);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = location.username }, location);
        }

        // DELETE: api/locations/5
        [ResponseType(typeof(location))]
        public IHttpActionResult Deletelocation(string id)
        {
            location location = db.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            db.Locations.Remove(location);
            db.SaveChanges();

            return Ok(location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool locationExists(string id)
        {
            return db.Locations.Count(e => e.username == id) > 0;
        }
    }
}