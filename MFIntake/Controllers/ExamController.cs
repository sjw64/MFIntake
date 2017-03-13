using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MFIntake.DAL;
using MFIntake.Models;

namespace MFIntake.Controllers
{
    public class ExamController : Controller
    {
        private IntakeContext db = new IntakeContext();

        // GET: Exam
        public ActionResult Index()
        {
            var exams = db.Exams.Include(e => e.Intake);
            return View(exams.ToList());
        }

        // GET: Exam/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // GET: Exam/Create
        public ActionResult Create()
        {
            ViewBag.IntakeID = new SelectList(db.Intakes, "ID", "CaseNumber");
            ViewBag.Analyst = new SelectList(db.Persons.Where(model => model.discriminator == "Analyst"), "ID", "FullName");
            ViewBag.StatusName = new SelectList(db.Statuses.Where(model => model.StatusType == "Exam"), "ID", "StatusName");
            return View();
        }

        // POST: Exam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExamID,IntakeID,ExamType,ExamStatus,Analyst,ExamDate,ExamFileLocation,AddlEquipNeeded,ExamNote")] Exam exam)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Exams.Add(exam);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //ViewBag.IntakeID = new SelectList(db.Intakes, "ID", "CaseNumber", exam.IntakeID);
            return View(exam);
        }

        // GET: Exam/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            ViewBag.IntakeID = new SelectList(db.Intakes, "ID", "CaseNumber", exam.IntakeID);
            return View(exam);
        }

        // POST: Exam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        //Edit([Bind(Include = "ExamID,IntakeID,ExamType,ExamStatus,Analyst,ExamDate,ExamFileLocation,AddlEquipNeeded,ExamNote")] Exam exam)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if (ModelState.IsValid)
            var examToUpdate = db.Exams.Find(id);
            if (TryUpdateModel(examToUpdate, "",
               new string[] { "IntakeID","ExamType","ExamStatus","Analyst","ExamDate","ExamFileLocation","AddlEquipNeeded","ExamNote" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(examToUpdate);
        }

        // GET: Exam/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // POST: Exam/Delete/5
        [HttpPost]
        //, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
            //DeleteConfirmed(int id)
        {
            try
            {
                Exam exam = db.Exams.Find(id);
                db.Exams.Remove(exam);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
