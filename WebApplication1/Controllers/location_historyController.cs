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
    public class location_historyController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/location_history
        public IQueryable<location_history> Getlocation_history()
        {
            return db.Location_history;
        }

        public IQueryable<location_history> GetLocationHistory(string title, double time)
        {
            DateTime datetime1 = DateTime.Now.AddMinutes(-time);
            DateTime datetime2 = DateTime.Now;
            return db.Location_history.Where(a => a.t_title.Equals(title) && a.datetime >= datetime1 && a.datetime <= datetime2);
        }
        // GET: api/location_history/5
        [ResponseType(typeof(location_history))]
        public IHttpActionResult Getlocation_history(string id)
        {
            location_history location_history = db.Location_history.Find(id);
            if (location_history == null)
            {
                return NotFound();
            }

            return Ok(location_history);
        }

        // PUT: api/location_history/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlocation_history(int id, location_history location_history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location_history.Id)
            {
                return BadRequest();
            }

            db.Entry(location_history).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!location_historyExists(id))
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

        // POST: api/location_history
        [ResponseType(typeof(location_history))]
        public IHttpActionResult PostLocalLocation(List<location_history> location_history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            for (int i = 0; i < location_history.Count; i++)

            { db.Location_history.Add(location_history[i]); }

            db.SaveChanges();


            return CreatedAtRoute("DefaultApi", new { id = location_history[0].Id }, location_history);
        }


        // DELETE: api/location_history/5
        [ResponseType(typeof(location_history))]
        public IHttpActionResult Deletelocation_history(string id)
        {
            location_history location_history = db.Location_history.Find(id);
            if (location_history == null)
            {
                return NotFound();
            }

            db.Location_history.Remove(location_history);
            db.SaveChanges();

            return Ok(location_history);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool location_historyExists(int id)
        {
            return db.Location_history.Count(e => e.Id == id) > 0;
        }
    }
}