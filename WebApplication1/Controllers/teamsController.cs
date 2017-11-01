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
    public class TeamsController : ApiController    
    {
        private CoTeamsRepository db = new CoTeamsRepository();

        // GET: api/teams
     
        public IQueryable<Teams> Getteams()
        {
            return db.GetInvisibleTeams();
        }
        public IQueryable<Teams> GetAllteams()
        {
            return db.GetAllTeams();
        }

        [ResponseType(typeof(Teams))]
        public IHttpActionResult Getteam(string id)
        {

            var query = db.FindInvisibleTeams(id);  
            if (query == null)
            {
                return Content(HttpStatusCode.NotFound, "Incorrect PIN!");
            }
            else
            {
                return Ok(query);
            }
        }

        // PUT: api/teams/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putteam(string id, Teams team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.Pin)
            {
                return BadRequest();
            }

            db.Entry(team).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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
        // POST: api/teams
        [ResponseType(typeof(Teams))]
        public IHttpActionResult Postteam(Teams team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Teams.Add(team);
            try
            {

                if (!db.IsCreator(team.Creator) && !db.TitleExists(team))
                {
                    db.SaveChanges();
                }

                else
                {
                    if (db.IsCreator(team.Creator))
                    { return Content(HttpStatusCode.Conflict, "You are Already in a Team!"); }

                    return Content(HttpStatusCode.Conflict, "Title Already Exists");
                }
            }
            catch (DbUpdateException)
            {
                if (TeamExists(team.Pin))
                {
                    return Content(HttpStatusCode.Conflict, "PIN Already Exists!");
                }
                else
                {
                    throw;
                }
            }


            db.InsertMember(team);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = team.Pin }, team);
        }


        [HttpDelete]
        public IHttpActionResult DeleteFromTeam(string user)
        {

            var t_member = db.T_members.Find(user);

            if (t_member == null)
            {
                return NotFound();
            }
            if (!db.IsCreator(user))

            {
                db.DeleteMember(user, t_member);
            }
            else
            {
                db.DeleteAssociateMembers(user);
            }

            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(string id)
        {
            return db.Teams.Count(e => e.Pin == id) > 0;
        }
    }
}