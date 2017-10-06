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
    public class items_historyController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/items_history
        public IQueryable<items_history> Getitems_history()
        {
            return db.items_history;
        }

        // GET: api/items_history/5
        [ResponseType(typeof(items_history))]
        public IHttpActionResult Getitems_history(string id)
        {
            items_history items_history = db.items_history.Find(id);
            if (items_history == null)
            {
                return NotFound();
            }

            return Ok(items_history);
        }

        // PUT: api/items_history/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putitems_history(string id, items_history items_history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != items_history.item_id)
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
                if (!items_historyExists(id))
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
        [ResponseType(typeof(items_history))]
        public IHttpActionResult Postitems_history(items_history items_history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.items_history.Add(items_history);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (items_historyExists(items_history.item_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = items_history.item_id }, items_history);
        }

        // DELETE: api/items_history/5
        [ResponseType(typeof(items_history))]
        public IHttpActionResult Deleteitems_history(string id)
        {
            items_history items_history = db.items_history.Find(id);
            if (items_history == null)
            {
                return NotFound();
            }

            db.items_history.Remove(items_history);
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

        private bool items_historyExists(string id)
        {
            return db.items_history.Count(e => e.item_id == id) > 0;
        }
    }
}