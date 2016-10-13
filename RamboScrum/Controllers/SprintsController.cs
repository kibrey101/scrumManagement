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
    public class SprintsController : Controller
    {
        private ScrumDatabaseEntities1 db = new ScrumDatabaseEntities1();

        // GET: Sprints
        public ActionResult Index()
        {
            var sprints = db.Sprints.Include(s => s.Project);
            return View(sprints.ToList());
        }

        // GET: Sprints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprint sprint = db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1");
            return View(sprint);
        }

        // GET: Sprints/Create
        public ActionResult Create()
        {
            ViewBag.project_id = new SelectList(db.Projects, "project_id", "project_name");
            return View();
        }

        // GET: Sprints/CreateForProject
        public ActionResult CreateForProject(int? project_id)
        {
            if (project_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(project_id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.project_name = project.project_name;
            ViewBag.project_id = project.project_id;
            return View();
        }

        // POST: Sprints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sprint_id,sprint_name,project_id,start_date,end_date")] Sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.Sprints.Add(sprint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.project_id = new SelectList(db.Projects, "project_id", "project_name", sprint.project_id);
            return View(sprint);
        }

        // POST: Sprints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForProject([Bind(Include = "sprint_id,sprint_name,project_id,start_date,end_date")] Sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.Sprints.Add(sprint);
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = sprint.project_id });
            }

            ViewBag.project_name = sprint.Project.project_name;
            ViewBag.project_id = sprint.project_id;
            return View(sprint);
        }

        // GET: Sprints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprint sprint = db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            ViewBag.project_id = new SelectList(db.Projects, "project_id", "project_name", sprint.project_id);
            return View(sprint);
        }

        // GET: Sprints/Edit/5
        public ActionResult EditForProject(int? sprint_id, int? project_id)
        {
            if (sprint_id == null || project_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprint sprint = db.Sprints.SingleOrDefault(m => m.sprint_id == sprint_id);
            Project project = db.Projects.SingleOrDefault(m => m.project_id == project_id);
            if (sprint == null || project == null)
            {
                return HttpNotFound();
            }
            ViewBag.project_name = project.project_name;
            ViewBag.project_id = project.project_id;
            return View(sprint);
        }

        // POST: Sprints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sprint_id,sprint_name,project_id,start_date,end_date")] Sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sprint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.project_id = new SelectList(db.Projects, "project_id", "project_name", sprint.project_id);
            return View(sprint);
        }

        // POST: Sprints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditForProject([Bind(Include = "sprint_id,sprint_name,project_id,start_date,end_date")] Sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sprint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = sprint.project_id });
            }
            ViewBag.project_name = sprint.Project.project_name;
            ViewBag.project_id = sprint.project_id;
            return View(sprint);
        }

        // GET: Sprints/Delete/5
        public ActionResult Delete(int? id, int? project_id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprint sprint = db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            return View(sprint);
        }

        // POST: Sprints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int project_id)
        {
            Sprint sprint = db.Sprints.Find(id);
            db.Sprints.Remove(sprint);
            db.SaveChanges();
            return RedirectToAction("Details", "Projects", new { id = project_id});
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
