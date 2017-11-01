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
    public class Items_historyController : ApiController
    {   
        private CoTeamsRepository db = new CoTeamsRepository();

        // GET: api/items_history
        public IQueryable<Items_history> Getitems_history()
        {
            return db.Items_history;
        }

        // GET: api/items_history/5
        [ResponseType(typeof(Items_history))]
        public IHttpActionResult Getitems_history(string id)
        {
            Items_history items_history = db.Items_history.Find(id);
            if (items_history == null)
            {
                return NotFound();
            }

            return Ok(items_history);
        }

        // PUT: api/items_history/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putitems_history(string id, Items_history items_history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != items_history.Item_id)
            {
                return BadRequest();
            }

            db.Entry(items_history).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Items_historyExists(id))
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

        // POST: api/items_history
        [ResponseType(typeof(Items_history))]
        public IHttpActionResult Postitems_history(Items_history items_history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items_history.Add(items_history);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Items_historyExists(items_history.Item_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = items_history.Item_id }, items_history);
        }

        // DELETE: api/items_history/5
        [ResponseType(typeof(Items_history))]
        public IHttpActionResult Deleteitems_history(string id)
        {
            Items_history items_history = db.Items_history.Find(id);
            if (items_history == null)
            {
                return NotFound();
            }

            db.Items_history.Remove(items_history);
            db.SaveChanges();

            return Ok(items_history);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Items_historyExists(string id)
        {
            return db.Items_history.Count(e => e.Item_id == id) > 0;
        }
    }
}