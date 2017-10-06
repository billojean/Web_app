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
    public class t_membersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/t_members
        public IQueryable<t_members> Gett_members()
        {
            return db.T_members;
        }


        public IHttpActionResult GetUserTeam(string user)
        {

            var member = db.FindMember(user);
            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }
        // GET: api/t_members/5
        [ResponseType(typeof(t_members))]
        public IHttpActionResult Get_member(string user)
        {
            t_members t_members = db.T_members.Find(user);
            if (t_members == null)
            {
                return NotFound();
            }

            return Ok(t_members);
        }


        // PUT: api/t_members/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putt_members(string id, t_members t_members)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_members.t_member)
            {
                return BadRequest();
            }

            db.Entry(t_members).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!t_membersExists(id))
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

        // POST: api/t_members
        [ResponseType(typeof(t_members))]
        public IHttpActionResult Post_member(t_members t_members)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_members.Add(t_members);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (t_membersExists(t_members.t_member))
                {
                    return Content(HttpStatusCode.Conflict, "You are Already in a Team!");
                }
                else
                {
                    throw;
                }
            }
            members_history members_history = new members_history
            {
                t_member = t_members.t_member,
                t_title = t_members.t_title,
                t_pin = t_members.t_pin,
                t_identity = "member",
                datetime_enter = DateTime.Now
            };

            db.members_history.Add(members_history);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = t_members.t_member }, t_members);
        }

        // DELETE: api/t_members/5
        [ResponseType(typeof(t_members))]
        public IHttpActionResult Delete(string id)
        {
            t_members t_members = db.T_members.Find(id);
            if (t_members == null)
            {
                return NotFound();
            }

            db.T_members.Remove(t_members);
            db.SaveChanges();

            return Ok(t_members);
        }
        [ResponseType(typeof(t_members))]


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool t_membersExists(string id)
        {
            return db.T_members.Count(e => e.t_member == id) > 0;
        }
    }
}