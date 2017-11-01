using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.DBA;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : ApiController 
    {
        private CoTeamsRepository db = new CoTeamsRepository();

        // GET: api/user
        public IQueryable<Users> Getusers()
        {
            return db.Users;
        }
        [HttpGet]
        public IHttpActionResult GetMembers(string user)
        {
            var member = db.FindTmember(user);

            if (member.Count() == 0)

            { return Ok(member); }//empty list

            var join = db.GetTeamMembersInfo(member);

            return Ok(join);
        }


        [HttpGet]
        public IHttpActionResult Getuser(string Email)
        {

            var usr = db.FindUserByEmail(Email);
            if (usr == null)

            {
                return NotFound();
            }
            else
            {
                return Ok(usr);
            }


        }

        // PUT: api/users/5

        [ResponseType(typeof(void))]
        public IHttpActionResult Putuser(int id, Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

      
        [HttpPost]
        public IHttpActionResult Login(Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var usr = db.Users.SingleOrDefault(u => u.UserName == user.UserName);
            if (user != null)
            {
                string savedPasswordHash = usr.PassWord;
                /* Extract the bytes */
                byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                /* Get the salt */
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                /* Compute the hash on the password the user entered */
                var pbkdf2 = new Rfc2898DeriveBytes(user.PassWord, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                /* Compare the results */
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return NotFound();
                    }
                }

                return Ok(usr);
            }
            else {
                return NotFound();
            }
        
        }

        // DELETE: api/users/5
        [ResponseType(typeof(Users))]
        public IHttpActionResult Deleteuser(int id)
        {
            Users user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}