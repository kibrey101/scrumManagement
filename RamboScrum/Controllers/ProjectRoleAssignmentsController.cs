using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RamboScrum.Models;
using System.Data.SqlClient;
namespace RamboScrum.Controllers
{
    public class ProjectRoleAssignmentsController : Controller
    {
        private ScrumDatabaseEntities1 db = new ScrumDatabaseEntities1();

        // GET: ProjectRoleAssignments
        public ActionResult Index()
        {
            var projectRoleAssignments = db.ProjectRoleAssignments.Include(p => p.Person).Include(p => p.Project).Include(p => p.ScrumRoleType);
            return View(projectRoleAssignments.ToList());
        }

        // GET: ProjectRoleAssignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectRoleAssignment projectRoleAssignment = db.ProjectRoleAssignments.Find(id);
            if (projectRoleAssignment == null)
            {
                return HttpNotFound();
            }
            return View(projectRoleAssignment);
        }

        // GET: ProjectRoleAssignments/Create
        public ActionResult Create()
        {
            ViewBag.person_id = new SelectList(db.People, "person_id", "first_name");
            ViewBag.project_id = new SelectList(db.Projects, "project_id", "project_name");
            ViewBag.scrum_role_type_id = new SelectList(db.ScrumRoleTypes, "scrum_role_type_id", "scrum_role_type_name");
            return View();
        }

        // GET: ProjectRoleAssignments/Create
        public ActionResult CreateNewRole(int? project_id)
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
            ViewBag.person_id = new SelectList(db.People, "person_id", "first_name");
            ViewBag.scrum_role_type_id = new SelectList(db.ScrumRoleTypes, "scrum_role_type_id", "scrum_role_type_name");
            return View();
        }

        // POST: ProjectRoleAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "project_id,person_id,scrum_role_type_id")] ProjectRoleAssignment projectRoleAssignment)
        {
            if (ModelState.IsValid)
            {
                db.ProjectRoleAssignments.Add(projectRoleAssignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.person_id = new SelectList(db.People, "person_id", "first_name", projectRoleAssignment.person_id);
            ViewBag.project_id = new SelectList(db.Projects, "project_id", "project_name", projectRoleAssignment.project_id);
            ViewBag.scrum_role_type_id = new SelectList(db.ScrumRoleTypes, "scrum_role_type_id", "scrum_role_type_name", projectRoleAssignment.scrum_role_type_id);
            return View(projectRoleAssignment);
        }

        // POST: ProjectRoleAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewRole([Bind(Include = "project_id,person_id,scrum_role_type_id")] ProjectRoleAssignment projectRoleAssignment)
        {
            if (ModelState.IsValid)
            {
                db.ProjectRoleAssignments.Add(projectRoleAssignment);
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = projectRoleAssignment.project_id });
            }
            ViewBag.project_name = projectRoleAssignment.Project.project_name;
            ViewBag.project_id = projectRoleAssignment.project_id;
            ViewBag.person_id = new SelectList(db.People, "person_id", "first_name", projectRoleAssignment.person_id);
            ViewBag.scrum_role_type_id = new SelectList(db.ScrumRoleTypes, "scrum_role_type_id", "scrum_role_type_name", projectRoleAssignment.scrum_role_type_id);
            return View(projectRoleAssignment);
        }

        // GET: ProjectRoleAssignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectRoleAssignment projectRoleAssignment = db.ProjectRoleAssignments.Find(id);
            if (projectRoleAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.person_id = new SelectList(db.People, "person_id", "first_name", projectRoleAssignment.person_id);
            ViewBag.project_id = new SelectList(db.Projects, "project_id", "project_name", projectRoleAssignment.project_id);
            ViewBag.scrum_role_type_id = new SelectList(db.ScrumRoleTypes, "scrum_role_type_id", "scrum_role_type_name", projectRoleAssignment.scrum_role_type_id);
            return View(projectRoleAssignment);
        }

        public ActionResult EditProjectRole(int? project_id, int? person_id, int? role_id)
        {
            if (project_id == null || person_id == null || role_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.SingleOrDefault(m => m.project_id == project_id);
            Person person = db.People.SingleOrDefault(m => m.person_id == person_id);
            ScrumRoleType role = db.ScrumRoleTypes.SingleOrDefault(m => m.scrum_role_type_id == role_id);
 
            if (project == null || person == null || role == null)
            {
                return HttpNotFound();
            }
            ProjectRoleAssignment projectRoleAssignment =db.ProjectRoleAssignments
        .SingleOrDefault(pra => pra.project_id == project_id && pra.person_id == person_id && pra.scrum_role_type_id == role_id);

            ViewBag.project_name = project.project_name;
            ViewBag.project_id = project.project_id;
            ViewBag.person_id = new SelectList(db.People, "person_id", "first_name", person.person_id);
            ViewBag.scrum_role_type_id = new SelectList(db.ScrumRoleTypes, "scrum_role_type_id", "scrum_role_type_name", role.scrum_role_type_id);

            return View(projectRoleAssignment);
        }

        // POST: ProjectRoleAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "project_id,person_id,scrum_role_type_id")] ProjectRoleAssignment projectRoleAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectRoleAssignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.person_id = new SelectList(db.People, "person_id", "first_name", projectRoleAssignment.person_id);
            ViewBag.project_id = new SelectList(db.Projects, "project_id", "project_name", projectRoleAssignment.project_id);
            ViewBag.scrum_role_type_id = new SelectList(db.ScrumRoleTypes, "scrum_role_type_id", "scrum_role_type_name", projectRoleAssignment.scrum_role_type_id);
            return View(projectRoleAssignment);
        }

        // POST: ProjectRoleAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProjectRole([Bind(Include = "project_id,person_id,scrum_role_type_id")] ProjectRoleAssignment projectRoleAssignment, int? oldProject_id, int? oldPerson_id, int? oldRole_id)
        {
            if (oldProject_id == null || oldPerson_id == null || oldRole_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectRoleAssignment myProjectRoleAssignment = db.ProjectRoleAssignments
       .SingleOrDefault(pra => pra.project_id == oldProject_id && pra.person_id == oldPerson_id && pra.scrum_role_type_id == oldRole_id);

            if (myProjectRoleAssignment != null)
            {
                db.ProjectRoleAssignments.Remove(myProjectRoleAssignment);
                db.SaveChanges();
            }

            if (ModelState.IsValid)
            {
                db.ProjectRoleAssignments.Add(projectRoleAssignment);
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = projectRoleAssignment.project_id });
            }
            ViewBag.project_name = projectRoleAssignment.Project.project_name;
            ViewBag.project_id = projectRoleAssignment.project_id;
            ViewBag.person_id = new SelectList(db.People, "person_id", "first_name", projectRoleAssignment.person_id);
            ViewBag.scrum_role_type_id = new SelectList(db.ScrumRoleTypes, "scrum_role_type_id", "scrum_role_type_name", projectRoleAssignment.scrum_role_type_id);
            return View(projectRoleAssignment);
        }

        // GET: ProjectRoleAssignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectRoleAssignment projectRoleAssignment = db.ProjectRoleAssignments.Find(id);
            if (projectRoleAssignment == null)
            {
                return HttpNotFound();
            }
            return View(projectRoleAssignment);
        }

        // GET: ProjectRoleAssignments/DeleteRole/
        public ActionResult DeleteRole(int? project_id, int? person_id, int? role_id)
        {
            if (project_id == null || person_id == null || role_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.SingleOrDefault(m => m.project_id == project_id);
            Person person = db.People.SingleOrDefault(m => m.person_id == person_id);
            ScrumRoleType role = db.ScrumRoleTypes.SingleOrDefault(m => m.scrum_role_type_id == role_id);
 
            if (project == null || person == null || role == null)
            {
                return HttpNotFound();
            }
            ProjectRoleAssignment projectRoleAssignment = db.ProjectRoleAssignments
       .SingleOrDefault(pra => pra.project_id == project_id && pra.person_id == person_id && pra.scrum_role_type_id == role_id);

            ViewBag.person = person;
            ViewBag.project = project;
            ViewBag.role = role;
            return View(projectRoleAssignment);
        }

        // POST: ProjectRoleAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectRoleAssignment projectRoleAssignment = db.ProjectRoleAssignments.Find(id);
            db.ProjectRoleAssignments.Remove(projectRoleAssignment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: ProjectRoleAssignments/Delete/5
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? project_id, int? person_id, int? role_id)
        {
            if (project_id == null || person_id == null || role_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.SingleOrDefault(m => m.project_id == project_id);
            Person person = db.People.SingleOrDefault(m => m.person_id == person_id);
            ScrumRoleType role = db.ScrumRoleTypes.SingleOrDefault(m => m.scrum_role_type_id == role_id);

            if (project == null || person == null || role == null)
            {
                return HttpNotFound();
            }
            ProjectRoleAssignment projectRoleAssignment = db.ProjectRoleAssignments
       .SingleOrDefault(pra => pra.project_id == project_id && pra.person_id == person_id && pra.scrum_role_type_id == role_id);
            db.ProjectRoleAssignments.Remove(projectRoleAssignment);
            db.SaveChanges();
            return RedirectToAction("Details", "Projects", new { id = project_id });

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
