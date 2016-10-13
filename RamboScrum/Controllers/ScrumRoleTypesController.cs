using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RamboScrum.Models;

namespace RamboScrum.Controllers
{
    public class ScrumRoleTypesController : Controller
    {
        private ScrumDatabaseEntities1 db = new ScrumDatabaseEntities1();

        // GET: ScrumRoleTypes
        public ActionResult Index()
        {
            return View(db.ScrumRoleTypes.ToList());
        }

        // GET: ScrumRoleTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScrumRoleType scrumRoleType = db.ScrumRoleTypes.Find(id);
            if (scrumRoleType == null)
            {
                return HttpNotFound();
            }
            return View(scrumRoleType);
        }

        // GET: ScrumRoleTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ScrumRoleTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "scrum_role_type_id,scrum_role_type_name")] ScrumRoleType scrumRoleType)
        {
            if (ModelState.IsValid)
            {
                db.ScrumRoleTypes.Add(scrumRoleType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(scrumRoleType);
        }

        // GET: ScrumRoleTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScrumRoleType scrumRoleType = db.ScrumRoleTypes.Find(id);
            if (scrumRoleType == null)
            {
                return HttpNotFound();
            }
            return View(scrumRoleType);
        }

        // POST: ScrumRoleTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "scrum_role_type_id,scrum_role_type_name")] ScrumRoleType scrumRoleType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scrumRoleType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scrumRoleType);
        }

        // GET: ScrumRoleTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScrumRoleType scrumRoleType = db.ScrumRoleTypes.Find(id);
            if (scrumRoleType == null)
            {
                return HttpNotFound();
            }
            return View(scrumRoleType);
        }

        // POST: ScrumRoleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScrumRoleType scrumRoleType = db.ScrumRoleTypes.Find(id);
            db.ScrumRoleTypes.Remove(scrumRoleType);
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
