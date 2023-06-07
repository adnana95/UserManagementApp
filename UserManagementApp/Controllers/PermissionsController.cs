using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserManagementApp.Data;
using UserManagementApp.Models;

namespace UserManagementApp.Controllers
{
    public class PermissionsController : Controller
    {
        private UserManagementAppContext db = new UserManagementAppContext();
        private static int userID;

        // GET: Permissions
        
        public ActionResult Index(int? id)
        {
            if(id != null) userID = (int)id;

            var permissions = db.PermissionModels.ToList().FindAll(x => x.UserModelID_FK == userID);
            if (permissions == null) permissions = new List<PermissionModel>();
            return View(permissions);
        }

        // GET: Permissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermissionModel permissionModel = db.PermissionModels.Find(id);
            if (permissionModel == null)
            {
                return HttpNotFound();
            }
            return View(permissionModel);
        }

        // GET: Permissions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Permissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Code,Description,UserModelID_FK")] PermissionModel permissionModel)
        {
            if (ModelState.IsValid)
            {
                permissionModel.UserModelID_FK = userID;
                db.PermissionModels.Add(permissionModel);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = userID });
            }

            return View(permissionModel);
        }

        // GET: Permissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermissionModel permissionModel = db.PermissionModels.Find(id);
            if (permissionModel == null)
            {
                return HttpNotFound();
            }
            return View(permissionModel);
        }

        // POST: Permissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Code,Description,UserModelID_FK")] PermissionModel permissionModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permissionModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(permissionModel);
        }

        // GET: Permissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermissionModel permissionModel = db.PermissionModels.Find(id);
            if (permissionModel == null)
            {
                return HttpNotFound();
            }
            return View(permissionModel);
        }

        // POST: Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PermissionModel permissionModel = db.PermissionModels.Find(id);
            db.PermissionModels.Remove(permissionModel);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = userID });
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
