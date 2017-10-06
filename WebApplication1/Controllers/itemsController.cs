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
    public class itemsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/items
        public IQueryable<items> Getitems(string user)
        {
            return db.Items.Where(u => u.item_owner == user);
        }

        // GET: api/items/5
        [ResponseType(typeof(items))]
        public IHttpActionResult Getitem(string id)
        {
            items item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
        [HttpGet]
        public IHttpActionResult Hasitems(string user)
        {

            var items = db.FindItem(user);
            if (items.Count() == 0)
            {
                return NotFound();
            }

            return Ok(items);

        }
        // PUT: api/items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putitem(string id, items item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.item_id)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!itemExists(id))
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

        // POST: api/items
        [ResponseType(typeof(items))]
        public IHttpActionResult Postitem(items item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(item);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (itemExists(item.item_id))
                {
                    return Content(HttpStatusCode.Conflict, "ID Already Exists!");
                }
                else
                {
                    throw;
                }
            }

            items_history items_history = new items_history
            {
                item_owner = item.item_owner,
                item_id = item.item_id,
                item_kind = item.item_kind,
                datetime_taken = DateTime.Now,
                datetime_return = null
            };
            if (itemExistsInHistory(item.item_id))
            {
                db.items_history.Attach(items_history);
                var entry = db.Entry(items_history);
                entry.Property(e => e.datetime_taken).IsModified = true;
                entry.Property(e => e.item_kind).IsModified = true;
                entry.Property(e => e.item_owner).IsModified = true;
                entry.Property(e => e.datetime_return).IsModified = true;


            }
            else
            {
                db.items_history.Add(items_history);

            }

            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = item.item_id }, item);
        }

        // DELETE: api/items/5

        // [ResponseType(typeof(items))]
        [HttpDelete]
        public IHttpActionResult Returnitem(string id)
        {
            items item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            db.SaveChanges();

            items_history items_history = new items_history
            {

                item_owner = item.item_owner,
                item_id = item.item_id,
                item_kind = item.item_kind,
                datetime_return = DateTime.Now
            };
            db.items_history.Attach(items_history);
            var entry = db.Entry(items_history);
            entry.Property(e => e.datetime_return).IsModified = true;
            db.SaveChanges();


            return Ok(item);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool itemExists(string id)
        {
            return db.Items.Count(e => e.item_id == id) > 0;
        }
        private bool itemExistsInHistory(string id)
        {
            return db.items_history.Count(e => e.item_id == id) > 0;
        }
    }
}