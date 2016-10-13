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
    public class TasksController : Controller
    {
        private ScrumDatabaseEntities1 db = new ScrumDatabaseEntities1();

        // GET: Tasks
        //public ActionResult Index()
        //{
        //    var tasks = db.Tasks.Include(t => t.PBLItem).Include(t => t.Person).Include(t => t.Status);
        //    return View(tasks.ToList());
        //}
        public ActionResult Index(int? pblItemId)
        {
            if (pblItemId != null)
            {
                var tasks = db.Tasks.Include(t => t.PBLItem).Include(t => t.Person).Include(t => t.Status).Where(t => t.pbl_item_id == pblItemId);
                ViewBag.pblItemId = pblItemId;
                return View(tasks.ToList());
            }
            else
            {
                var tasks = db.Tasks.Include(t => t.PBLItem).Include(t => t.Person).Include(t => t.Status);
                return View(tasks.ToList());
            }          
        }



        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.pbl_item_id = new SelectList(db.PBLItems, "pbl_item_id", "name");
            ViewBag.person_id = new SelectList(db.People, "person_id", "first_name");
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1");
            return View();
        }

        public ActionResult CreateForPerson(int? person_id)
        {
            if (person_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(person_id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.person_firstname = person.first_name;
            ViewBag.person_lastname = person.last_name;

            ViewBag.person_id = person.person_id;
            ViewBag.pbl_item_id = new SelectList(db.PBLItems, "pbl_item_id", "name");
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1");
            return View();
        }
        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "task_id,name,task_description,estimated_hours,task_priority,pbl_item_id,status_id,person_id,assignment_date,completion_date")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pbl_item_id = new SelectList(db.PBLItems, "pbl_item_id", "name", task.pbl_item_id);
            ViewBag.person_id = new SelectList(db.People, "person_id", "first_name", task.person_id);
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1", task.status_id);
            return View(task);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForPerson([Bind(Include = "task_id,name,task_description,estimated_hours,task_priority,pbl_item_id,status_id,person_id,assignment_date,completion_date")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Details", "People", new { id = task.person_id });
            }

            ViewBag.pbl_item_id = new SelectList(db.PBLItems, "pbl_item_id", "name", task.pbl_item_id);
            ViewBag.person_id = new SelectList(db.People, "person_id", "first_name", task.person_id);
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1", task.status_id);
            return View(task);
        }
        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.pbl_item_id = new SelectList(db.PBLItems, "pbl_item_id", "name", task.pbl_item_id);
            ViewBag.person_id = new SelectList(db.People, "person_id", "first_name", task.person_id);
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1", task.status_id);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "task_id,name,task_description,estimated_hours,task_priority,pbl_item_id,status_id,person_id,assignment_date,completion_date")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pbl_item_id = new SelectList(db.PBLItems, "pbl_item_id", "name", task.pbl_item_id);
            ViewBag.person_id = new SelectList(db.People, "person_id", "first_name", task.person_id);
            ViewBag.status_id = new SelectList(db.Status, "status_id", "status1", task.status_id);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
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
