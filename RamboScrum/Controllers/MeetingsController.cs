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
    public class MeetingsController : Controller
    {
        private ScrumDatabaseEntities1 db = new ScrumDatabaseEntities1();

        // GET: Meetings
        public ActionResult Index()
        {
            var meetings = db.Meetings.Include(m => m.MeetingType).Include(m => m.Sprint);
            return View(meetings.ToList());
        }

        // GET: Meetings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // GET: Meetings/Create
        public ActionResult Create(int? id)
        {
            ViewBag.meeting_type_id = new SelectList(db.MeetingTypes, "meeting_type_id", "meeting_type_name");
            ViewBag.sprint_id = new SelectList(db.Sprints, "sprint_id", "sprint_name");

            //Sprint sprint = db.Sprints.Find(id);
            //if (sprint == null)
            //{
            //    return HttpNotFound();
            //}

            return View();
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "meeting_id,meeting_type_id,sprint_id,start_time")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                db.Meetings.Add(meeting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.meeting_type_id = new SelectList(db.MeetingTypes, "meeting_type_id", "meeting_type_name", meeting.meeting_type_id);
            ViewBag.sprint_id = new SelectList(db.Sprints, "sprint_id", "sprint_name", meeting.sprint_id);
            return View(meeting);
        }

        // GET: Meetings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            ViewBag.meeting_type_id = new SelectList(db.MeetingTypes, "meeting_type_id", "meeting_type_name", meeting.meeting_type_id);
            ViewBag.sprint_id = new SelectList(db.Sprints, "sprint_id", "sprint_name", meeting.sprint_id);
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "meeting_id,meeting_type_id,sprint_id,start_time")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meeting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.meeting_type_id = new SelectList(db.MeetingTypes, "meeting_type_id", "meeting_type_name", meeting.meeting_type_id);
            ViewBag.sprint_id = new SelectList(db.Sprints, "sprint_id", "sprint_name", meeting.sprint_id);
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            db.Meetings.Remove(meeting);
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
