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
    public class ProjectsController : Controller
    {
        private ScrumDatabaseEntities1 db = new ScrumDatabaseEntities1();

        // GET: Projects
        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.DefinitionOfDone);
            return View(projects.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(int? pblItem_id, int? status_id)
        {
            if (pblItem_id == null || status_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var pblItem = db.PBLItems.SingleOrDefault(m => m.pbl_item_id == pblItem_id);
                pblItem.status_id = Convert.ToInt32(status_id);
                db.Entry(pblItem).State = EntityState.Modified;
                db.SaveChanges();
                if (status_id == 1)
                    ViewBag.myClass = "Planned";
                else if(status_id == 2)
                    ViewBag.myClass = "Ongoing";
                else
                    ViewBag.myClass = "Done";
                ViewBag.status_id = new SelectList(db.Status, "status_id", "status1");
                return PartialView("_ItemStatusUpdated", pblItem);
                
            }
        }
        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            //Status status = db.Status.SingleOrDefault();

            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1");
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.definition_of_done_id = new SelectList(db.DefinitionOfDones, "definition_of_done_id", "definition_of_done_source");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "project_id,definition_of_done_id,project_name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.definition_of_done_id = new SelectList(db.DefinitionOfDones, "definition_of_done_id", "definition_of_done_id", project.definition_of_done_id);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.definition_of_done_id = new SelectList(db.DefinitionOfDones, "definition_of_done_id", "definition_of_done_id", project.definition_of_done_id);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "project_id,definition_of_done_id,project_name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.definition_of_done_id = new SelectList(db.DefinitionOfDones, "definition_of_done_id", "definition_of_done_id", project.definition_of_done_id);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.PBLItems.RemoveRange(db.PBLItems.Where(m => m.project_id == id));
            db.SaveChanges();
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Projects/ShowItems
        public PartialViewResult ShowItems() {
            return PartialView("_PBLItemsOfProject");
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
