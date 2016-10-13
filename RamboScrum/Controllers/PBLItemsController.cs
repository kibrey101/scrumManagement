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
    public class PBLItemsController : Controller
    {
        private ScrumDatabaseEntities1 db = new ScrumDatabaseEntities1();

        // GET: PBLItems
        public ActionResult Index()
        {
            var pBLItems = db.PBLItems.Include(p => p.Project).Include(p => p.Sprint).Include(p => p.Status);
            return View(pBLItems.ToList());
        }


        // GET: PBLItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PBLItem pBLItem = db.PBLItems.Find(id);
            if (pBLItem == null)
            {
                return HttpNotFound();
            }
            return View(pBLItem);
        }

        // GET: PBLItems/Create
        public ActionResult Create()
        {
            ViewBag.project_id = new SelectList(db.Projects, "project_id", "project_name");
            ViewBag.sprint_id = new SelectList(db.Sprints, "sprint_id", "sprint_name");
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1");
            return View();
        }

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

            ViewBag.project_name= project.project_name;
            ViewBag.project_id = project.project_id;
            ViewBag.sprint_id = new SelectList(db.Sprints.Where(m => m.project_id == project_id), "sprint_id", "sprint_name");
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1");
            return View();
        }

        // POST: PBLItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pbl_item_id,project_id,sprint_id,status_id,item_priority,estimated_hours,name,item_description")] PBLItem pBLItem)
        {
            if (ModelState.IsValid)
            {
                db.PBLItems.Add(pBLItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.project_id = new SelectList(db.Projects, "project_id", "project_name", pBLItem.project_id);
            ViewBag.sprint_id = new SelectList(db.Sprints, "sprint_id", "sprint_name", pBLItem.sprint_id);
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1", pBLItem.status_id);
            return View(pBLItem);
        }

        // POST: PBLItems/CreateForProject
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForProject([Bind(Include = "pbl_item_id,project_id,sprint_id,status_id,item_priority,estimated_hours,name,item_description")] PBLItem pBLItem)
        {
            if (ModelState.IsValid)
            {
                db.PBLItems.Add(pBLItem);
                db.SaveChanges();
                return RedirectToAction("Details","Projects", new { id = pBLItem.project_id });
            }

            ViewBag.project_name = pBLItem.Project.project_name;
            ViewBag.project_id = pBLItem.project_id;
            ViewBag.sprint_id = new SelectList(db.Sprints, "sprint_id", "sprint_name", pBLItem.sprint_id);
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1", pBLItem.status_id);
            return View(pBLItem);
        }

        // GET: PBLItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PBLItem pBLItem = db.PBLItems.Find(id);
            if (pBLItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.project_id = new SelectList(db.Projects, "project_id", "project_name", pBLItem.project_id);
            ViewBag.sprint_id = new SelectList(db.Sprints, "sprint_id", "sprint_name", pBLItem.sprint_id);
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1", pBLItem.status_id);
            return View(pBLItem);
        }

        // GET: PBLItems/EditForProject/5
        public ActionResult EditForProject(int? item_id, int? project_id)
        {
            if (item_id == null || project_id ==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PBLItem pBLItem = db.PBLItems.SingleOrDefault(m => m.pbl_item_id == item_id);
            Project project = db.Projects.SingleOrDefault(m => m.project_id == project_id);
            if (pBLItem == null || project == null)
            {
                return HttpNotFound();
            }
            ViewBag.project_name = project.project_name;
            ViewBag.project_id = project.project_id;
            ViewBag.sprint_id = new SelectList(db.Sprints.Where(m => m.project_id == project_id), "sprint_id", "sprint_name");
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1");
            return View(pBLItem);
        }

        // POST: PBLItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pbl_item_id,project_id,sprint_id,status_id,item_priority,estimated_hours,name,item_description")] PBLItem pBLItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pBLItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.project_id = new SelectList(db.Projects, "project_id", "project_name", pBLItem.project_id);
            ViewBag.sprint_id = new SelectList(db.Sprints, "sprint_id", "sprint_name", pBLItem.sprint_id);
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1", pBLItem.status_id);
            return View(pBLItem);
        }

        // POST: PBLItems/EditForProject/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditForProject([Bind(Include = "pbl_item_id,project_id,sprint_id,status_id,item_priority,estimated_hours,name,item_description")] PBLItem pBLItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pBLItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = pBLItem.project_id });
            }
            ViewBag.project_id = new SelectList(db.Projects, "project_id", "project_name", pBLItem.project_id);
            ViewBag.sprint_id = new SelectList(db.Sprints, "sprint_id", "sprint_name", pBLItem.sprint_id);
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1", pBLItem.status_id);
            return View(pBLItem);
        }

        // GET: PBLItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PBLItem pBLItem = db.PBLItems.Find(id);
            if (pBLItem == null)
            {
                return HttpNotFound();
            }
            return View(pBLItem);
        }

        // POST: PBLItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task taskItem = db.Tasks.Find(id);
            PBLItem pBLItem = db.PBLItems.Find(id);
            db.PBLItems.Remove(pBLItem);
            db.SaveChanges();
            return RedirectToRoute(new
            {
                controller = "Projects",
                action = "Details/"+pBLItem.project_id
            });
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
