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
    public class DefinitionOfDonesController : Controller
    {
        private ScrumDatabaseEntities1 db = new ScrumDatabaseEntities1();

        // GET: DefinitionOfDones
        public ActionResult Index()
        {
            return View(db.DefinitionOfDones.ToList());
        }

        // GET: DefinitionOfDones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefinitionOfDone definitionOfDone = db.DefinitionOfDones.Find(id);
            if (definitionOfDone == null)
            {
                return HttpNotFound();
            }
            return View(definitionOfDone);
        }

        // GET: DefinitionOfDones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DefinitionOfDones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "definition_of_done_id")] DefinitionOfDone definitionOfDone)
        {
            if (ModelState.IsValid)
            {
                db.DefinitionOfDones.Add(definitionOfDone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(definitionOfDone);
        }

        // GET: DefinitionOfDones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefinitionOfDone definitionOfDone = db.DefinitionOfDones.Find(id);
            if (definitionOfDone == null)
            {
                return HttpNotFound();
            }
            return View(definitionOfDone);
        }

        // POST: DefinitionOfDones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "definition_of_done_id")] DefinitionOfDone definitionOfDone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(definitionOfDone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(definitionOfDone);
        }

        // GET: DefinitionOfDones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefinitionOfDone definitionOfDone = db.DefinitionOfDones.Find(id);
            if (definitionOfDone == null)
            {
                return HttpNotFound();
            }
            return View(definitionOfDone);
        }

        // POST: DefinitionOfDones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DefinitionOfDone definitionOfDone = db.DefinitionOfDones.Find(id);
            db.DefinitionOfDones.Remove(definitionOfDone);
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
