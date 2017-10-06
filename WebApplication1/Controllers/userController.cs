using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;
using WebApplication1.DBA;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class userController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/user
        public IQueryable<users> Getusers()
        {
            return db.Users;
        }
        [HttpGet]
        public IHttpActionResult GetMembers(string user)
        {
            var query = db.FindTmember(user);

            if (query.Count() == 0)

            { return Ok(query); }//empty list

            string pin = query.First().t_pin;



            var join = from t1 in db.T_members
                       join t2 in db.Users on t1.t_member equals t2.UserName
                       where t1.t_pin == pin join t3 in db.departments on t2.DId equals t3.DId
                       orderby t1.t_identity
                       select new { t2.FirstName, t2.LastName, t1.t_title, t1.t_member, t1.t_identity, t2.Email, t2.OfficePhone, t2.MobilePhone,t3.Department, t2.Pic };


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
        public IHttpActionResult Putuser(int id, users user)
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
                if (!userExists(id))
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
        public IHttpActionResult Login(users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /* var usr = db.FindUserByPasswd(user);

             if (usr == null)
             {
                 return NotFound();
             }
             else
             {

                 return Ok(usr);
             }*/
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
        [ResponseType(typeof(users))]
        public IHttpActionResult Deleteuser(int id)
        {
            users user = db.Users.Find(id);
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

        private bool userExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}