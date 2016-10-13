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
    public class MeetingTypesController : Controller
    {
        private ScrumDatabaseEntities1 db = new ScrumDatabaseEntities1();

        // GET: MeetingTypes
        public ActionResult Index()
        {
            return View(db.MeetingTypes.ToList());
        }

        // GET: MeetingTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingType meetingType = db.MeetingTypes.Find(id);
            if (meetingType == null)
            {
                return HttpNotFound();
            }
            return View(meetingType);
        }

        // GET: MeetingTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeetingTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "meeting_type_id,meeting_type_name")] MeetingType meetingType)
        {
            if (ModelState.IsValid)
            {
                db.MeetingTypes.Add(meetingType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meetingType);
        }

        // GET: MeetingTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingType meetingType = db.MeetingTypes.Find(id);
            if (meetingType == null)
            {
                return HttpNotFound();
            }
            return View(meetingType);
        }

        // POST: MeetingTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "meeting_type_id,meeting_type_name")] MeetingType meetingType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meetingType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meetingType);
        }

        // GET: MeetingTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingType meetingType = db.MeetingTypes.Find(id);
            if (meetingType == null)
            {
                return HttpNotFound();
            }
            return View(meetingType);
        }

        // POST: MeetingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeetingType meetingType = db.MeetingTypes.Find(id);
            db.MeetingTypes.Remove(meetingType);
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
