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
    public class SparePartsController : ApiController
    {
        private CoTeamsRepository db = new CoTeamsRepository();

        // GET: api/SpareParts
        public IQueryable<SpareParts> Getspareparts()
        {
            return db.Spareparts;
        }

        // GET: api/SpareParts/5
        [ResponseType(typeof(SpareParts))]
        public IHttpActionResult GetSpareParts(string id)
        {
            SpareParts spareparts = db.Spareparts.Find(id);
            if (spareparts == null)
            {
                return NotFound();
            }

            return Ok(spareparts);
        }

        // PUT: api/SpareParts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSpareParts(string id, SpareParts spareparts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != spareparts.Id)
            {
                return BadRequest();
            }

            db.Entry(spareparts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SparePartsExists(id))
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

        // POST: api/SpareParts
        [ResponseType(typeof(SpareParts))]
        public IHttpActionResult PostSpareParts(SpareParts spareparts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Spareparts.Add(spareparts);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SparePartsExists(spareparts.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = spareparts.Id }, spareparts);
        }

        // DELETE: api/SpareParts/5
        [ResponseType(typeof(SpareParts))]
        public IHttpActionResult DeleteSpareParts(string id)
        {
            SpareParts spareparts = db.Spareparts.Find(id);
            if (spareparts == null)
            {
                return NotFound();
            }

            db.Spareparts.Remove(spareparts);
            db.SaveChanges();

            return Ok(spareparts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SparePartsExists(string id)
        {
            return db.Spareparts.Count(e => e.Id == id) > 0;
        }
    }
}