using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.DBA;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LocationsController : ApiController
    {
        private CoTeamsRepository db = new CoTeamsRepository();

        // GET: api/locations
        [ResponseType(typeof(Location))]
        public IHttpActionResult Getlocations(string user)
        {
            var member = db.FindMember(user);   
            if (member == null)
            {
                return NotFound();
            }

            string title = member.T_title;  

            var getTeammembers = db.FindTeamMembers(user, title);

            if (getTeammembers.Count() == 0)
            {
                return NotFound();
            }
            return Ok(getTeammembers);
        }

        public IQueryable <Location> GetTeamMembersLocation(string title)
        {
            return db.GetTeamLocations(title);    
        }

        // GET: api/locations/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult Getlocation(string id)
        {
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/locations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlocation(string id, Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.Username)
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
                if (!LocationExists(id))
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
        [ResponseType(typeof(Location))]
        public IHttpActionResult Postlocation(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            db.AddLocation(location);

            return CreatedAtRoute("DefaultApi", new { id = location.Username }, location);
        }

        // DELETE: api/locations/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult Deletelocation(string id)
        {
            Location location = db.Locations.Find(id);
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

        private bool LocationExists(string id)
        {
            return db.Locations.Count(e => e.Username == id) > 0;
        }
    }
}