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
    public class LaptopsController : ApiController
    {
        private CoTeamsRepository db = new CoTeamsRepository();

        // GET: api/Laptops
        public IQueryable<Laptops> Getlaptops()
        {
            return db.Laptops;
        }

        // GET: api/Laptops/5
        [ResponseType(typeof(Laptops))]
        public IHttpActionResult GetLaptops(string id)
        {
            Laptops laptops = db.Laptops.Find(id);
            if (laptops == null)
            {
                return NotFound();
            }

            return Ok(laptops);
        }

        // PUT: api/Laptops/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLaptops(string id, Laptops laptops)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != laptops.Id)
            {
                return BadRequest();
            }

            db.Entry(laptops).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LaptopsExists(id))
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

        // POST: api/Laptops
        [ResponseType(typeof(Laptops))]
        public IHttpActionResult PostLaptops(Laptops laptops)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Laptops.Add(laptops);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LaptopsExists(laptops.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = laptops.Id }, laptops);
        }

        // DELETE: api/Laptops/5
        [ResponseType(typeof(Laptops))]
        public IHttpActionResult DeleteLaptops(string id)
        {
            Laptops laptops = db.Laptops.Find(id);
            if (laptops == null)
            {
                return NotFound();
            }

            db.Laptops.Remove(laptops);
            db.SaveChanges();

            return Ok(laptops);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LaptopsExists(string id)
        {
            return db.Laptops.Count(e => e.Id == id) > 0;
        }
    }
}