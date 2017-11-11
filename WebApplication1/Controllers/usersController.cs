using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.DBA;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataContext db;

        public UsersController()
        {
            this.db =new DataContext();
        }
       // private DataContext db = new DataContext(); 

        // GET: users
        [Authorize]
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Departments);
            return View(users.ToList());
        }

        // GET: users/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: users/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.DId = new SelectList(db.Departments, "DId", "Department");
            return View();
        }



        // POST: users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,UserName,PassWord,Email,OfficePhone,MobilePhone,DId", Exclude = "Pic")] Users users)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["Pic"];

                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }

                if (users.PassWord != null)
                { users.PassWord = Encrypt(users.PassWord); }
                users.Pic = imageData;
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DId = new SelectList(db.Departments, "DId", "Department", users.DId);
            return View(users);
        }

       
        // GET: users/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            users.PassWord = null;
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.DId = new SelectList(db.Departments, "DId", "Department", users.DId);
            return View(users);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,UserName,PassWord,Email,OfficePhone,MobilePhone,DId", Exclude = "Pic")] Users users)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["Pic"];

                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }

                }if (users.PassWord != null)
                { users.PassWord = Encrypt(users.PassWord); }              
                 users.Pic = imageData; 
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DId = new SelectList(db.Departments, "DId", "Department", users.DId);
            return View(users);
        }

        // GET: users/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private string Encrypt(string passWord)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(passWord, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }
    }
}
