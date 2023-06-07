using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using UserManagementApp.Data;
using UserManagementApp.Models;

namespace UserManagementApp.Controllers
{
    public class UsersController : Controller
    {
        private UserManagementAppContext db = new UserManagementAppContext();
        private const int pageSize = 10;

        // GET: Users
        public ActionResult Index(int page = 0, string sortOrder = "", string sortParameter = "", string searchString = "")
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["SortOrder"] = sortOrder;
            ViewData["SortParameter"] = sortParameter;

         //   var count = db.UserModels.Count();
            var data = from u in db.UserModels select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                data = data.Where(u => u.LastName.Contains(searchString) || u.FirstName.Contains(searchString));
            }

            data = UserManagementApp.Helpers.Sorting.SortUsersData(data, sortParameter, sortOrder);

            var count = data.Count();
            var dataForPage = data.Skip(page * pageSize).Take(pageSize).ToList();            

            ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
            ViewBag.Page = page;

            return View(dataForPage);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.UserModels.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Username,Password,Email,Status")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                db.UserModels.Add(userModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userModel);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.UserModels.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel userModel)
        {
            var userInDB = db.UserModels.ToList().Find(x => x.ID == userModel.ID);
            userInDB.FirstName = userModel.FirstName;
            userInDB.LastName = userModel.LastName;
            userInDB.Email = userModel.Email;
            userInDB.Status = userModel.Status;

            db.Entry(userInDB).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");    
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.UserModels.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            
            return View(userModel);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserModel userModel = db.UserModels.Find(id);
            db.UserModels.Remove(userModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       
    }
}
