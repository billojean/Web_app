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
    public class T_membersController : ApiController
    {
        private CoTeamsRepository db = new CoTeamsRepository();

        // GET: api/t_members
        public IQueryable<T_members> Gett_members()
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
        [ResponseType(typeof(T_members))]
        public IHttpActionResult Get_member(string user)
        {
            var t_members = db.T_members.Find(user);    
            if (t_members == null)
            {
                return NotFound();
            }

            return Ok(t_members);
        }


        // PUT: api/t_members/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putt_members(string id, T_members t_members)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_members.T_member)
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
                if (!T_membersExists(id))
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
        [ResponseType(typeof(T_members))]
        public IHttpActionResult Post_member(T_members t_members)
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
                if (T_membersExists(t_members.T_member))
                {
                    return Content(HttpStatusCode.Conflict, "You are Already in a Team!");
                }
                else
                {
                    throw;
                }
            }
            db.AddMemberHistory(t_members);

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = t_members.T_member }, t_members);
        }

        // DELETE: api/t_members/5
        [ResponseType(typeof(T_members))]
        public IHttpActionResult Delete(string id)
        {
            T_members t_members = db.T_members.Find(id);
            if (t_members == null)
            {
                return NotFound();
            }

            db.T_members.Remove(t_members);
            db.SaveChanges();

            return Ok(t_members);
        }
        [ResponseType(typeof(T_members))]


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_membersExists(string id)
        {
            return db.T_members.Count(e => e.T_member == id) > 0;
        }
    }
}