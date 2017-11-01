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
    public class ItemsController : ApiController    
    {
        private CoTeamsRepository db = new CoTeamsRepository();

        // GET: api/items
        public IQueryable<Items> Getitems(string user)
        {

            return db.GetUserItems(user);
        }

        // GET: api/items/5
        [ResponseType(typeof(Items))]
        public IHttpActionResult Getitem(string id)
        {
            Items item = db.Items.Find(id);
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
        public IHttpActionResult Putitem(string id, Items item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Item_id)
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
                if (!ItemExists(id))
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
        [ResponseType(typeof(Items))]
        public IHttpActionResult Postitem(Items item)
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
                if (ItemExists(item.Item_id))
                {
                    return Content(HttpStatusCode.Conflict, "ID Already Exists!");
                }
                else
                {
                    throw;
                }
            }

            db.InsertItemHistory(item);

            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = item.Item_id }, item);
        }

        // DELETE: api/items/5

        [HttpDelete]
        public IHttpActionResult Returnitem(string id)
        {
            Items item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);

            db.ReturnInItemHistory(item);

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

        private bool ItemExists(string id)
        {
            return db.Items.Count(e => e.Item_id == id) > 0;
        }
       
    }
}