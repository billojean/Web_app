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
    public class VehiclesController : ApiController
    {
        private CoTeamsRepository db = new CoTeamsRepository();

        // GET: api/Vehicles
        public IQueryable<Vehicles> Getvehicles()
        {
            return db.Vehicles;
        }

        // GET: api/Vehicles/5
        [ResponseType(typeof(Vehicles))]
        public IHttpActionResult GetVehicles(string id)
        {
            Vehicles vehicles = db.Vehicles.Find(id);
            if (vehicles == null)
            {
                return NotFound();
            }

            return Ok(vehicles);
        }

        // PUT: api/Vehicles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVehicles(string id, Vehicles vehicles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicles.Id)
            {
                return BadRequest();
            }

            db.Entry(vehicles).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiclesExists(id))
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

        // POST: api/Vehicles
        [ResponseType(typeof(Vehicles))]
        public IHttpActionResult PostVehicles(Vehicles vehicles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vehicles.Add(vehicles);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VehiclesExists(vehicles.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vehicles.Id }, vehicles);
        }

        // DELETE: api/Vehicles/5
        [ResponseType(typeof(Vehicles))]
        public IHttpActionResult DeleteVehicles(string id)
        {
            Vehicles vehicles = db.Vehicles.Find(id);
            if (vehicles == null)
            {
                return NotFound();
            }

            db.Vehicles.Remove(vehicles);
            db.SaveChanges();

            return Ok(vehicles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehiclesExists(string id)
        {
            return db.Vehicles.Count(e => e.Id == id) > 0;
        }
    }
}