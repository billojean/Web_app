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
    public class teamsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/teams
     
        public IQueryable<teams> Getteams()
        {
            return db.Teams.Where(u => u.visibility == "true");
        }
        public IQueryable<teams> GetAllteams()
        {
            return db.Teams.OrderBy(a => a.title);
        }

        [ResponseType(typeof(teams))]
        public IHttpActionResult Getteam(string id)
        {

            var query = db.FindInvTeams(id);
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
        public IHttpActionResult Putteam(string id, teams team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.pin)
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
                if (!teamExists(id))
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
        [ResponseType(typeof(teams))]
        public IHttpActionResult Postteam(teams team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Teams.Add(team);
            try
            {

                if (!db.InTeam(team) && !db.titleExists(team))
                {
                    db.SaveChanges();
                }

                else
                {
                    if (db.InTeam(team))
                    { return Content(HttpStatusCode.Conflict, "You are Already in a Team!"); }

                    return Content(HttpStatusCode.Conflict, "Title Already Exists");
                }
            }
            catch (DbUpdateException)
            {
                if (teamExists(team.pin))
                {
                    return Content(HttpStatusCode.Conflict, "PIN Already Exists!");
                }
                else
                {
                    throw;
                }
            }



            t_members t_members = new t_members
            {
                t_member = team.creator,
                t_title = team.title,
                t_pin = team.pin,
                t_identity = "creator"

            };
            members_history members_history = new members_history
            {
                t_member = team.creator,
                t_title = team.title,
                t_pin = team.pin,
                t_identity = "creator",
                datetime_enter = DateTime.Now
            };

            db.T_members.Add(t_members);
            db.members_history.Add(members_history);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = team.pin }, team);
        }


        [HttpDelete]
        public IHttpActionResult DeleteFromTeam(string user)
        {
            var query = db.Teams.SingleOrDefault(u => u.creator == user);

            t_members t_member = db.T_members.Find(user);

            if (t_member == null)
            {
                return NotFound();
            }
            if (query == null)

            {
                db.T_members.Remove(t_member);

                members_history result = (from p in db.members_history
                                          where p.t_member == user && p.datetime_leave == null
                                          select p).SingleOrDefault();

                result.datetime_leave = DateTime.Now;

            }
            else
            {
                var pin = query.pin;
                db.Teams.RemoveRange(db.Teams.Where(u => u.pin == pin));
                db.T_members.RemoveRange(db.T_members.Where(u => u.t_pin == pin));

                (from p in db.members_history
                 where p.t_pin == pin && p.datetime_leave == null
                 select p).ToList().ForEach(x => x.datetime_leave = DateTime.Now);

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

        private bool teamExists(string id)
        {
            return db.Teams.Count(e => e.pin == id) > 0;
        }
    }
}