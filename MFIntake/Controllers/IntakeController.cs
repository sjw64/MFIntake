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

    public class IntakeController : Controller
    {
        private IntakeContext db = new IntakeContext();

        // GET: Intake
        public ActionResult Index(string sortOrder)
        {
            ViewBag.CaseSortParm = string.IsNullOrEmpty(sortOrder) ? "case_desc" : "";
            ViewBag.DeviceSortParm = string.IsNullOrEmpty(sortOrder) ? "device_desc" : "";
            ViewBag.AgentSortParm = string.IsNullOrEmpty(sortOrder) ? "agent_desc" : "";
            ViewBag.CustodianSortParm = string.IsNullOrEmpty(sortOrder) ? "cust_desc" : "";
            ViewBag.WarrantSortParm = string.IsNullOrEmpty(sortOrder) ? "warr_desc" : "";
            ViewBag.LocSortParm = string.IsNullOrEmpty(sortOrder) ? "loc_desc" : "";
            ViewBag.StatusSortParm = string.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
            ViewBag.RecSortParm = sortOrder == "Date" ? "rec_desc" : "Date";
            ViewBag.ReqSortParm = sortOrder == "Date" ? "req_desc" : "Date";

            var intakes = from e in db.Intakes
                           select e;
            switch (sortOrder)
            {
                case "case_desc":
                    intakes = intakes.OrderByDescending(e => e.CaseNumber);
                    break;
                case "device_desc":
                    intakes = intakes.OrderByDescending(e => e.DeviceModel);
                    break;
                case "agent_desc":
                    intakes = intakes.OrderByDescending(e => e.FullName);
                    break;
                case "cust_desc":
                    intakes = intakes.OrderByDescending(e => e.Custodian);
                    break;
                case "warr_desc":
                    intakes = intakes.OrderByDescending(e => e.WarrantNumber);
                    break;
                case "loc_desc":
                    intakes = intakes.OrderByDescending(e => e.StorageLocation);
                    break;
                case "status_desc":
                    intakes = intakes.OrderByDescending(e => e.IntakeStatus);
                    break;
                case "rec_desc":
                    intakes = intakes.OrderByDescending(e => e.ReceivedDate);
                    break;
                case "req_desc":
                    intakes = intakes.OrderByDescending(e => e.RequestedByDate);
                    break;
                default:
                    intakes = intakes.OrderBy(s => s.CaseNumber);
                    break;
            }
            return View(intakes.ToList());
        }

        // GET: Intake/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Intake intake = db.Intakes.Find(id);
            if (intake == null)
            {
                return HttpNotFound();
            }
            return View(intake);
        }

        // GET: Intake/Create
        public ActionResult Create()
        {
            ViewBag.FullName = new SelectList(db.Agents, "ID", "FullName");
            ViewBag.Custodian = new SelectList(db.Custodians, "ID", "FullName");
            ViewBag.StatusName = new SelectList(db.Statuses.Where(model => model.StatusType == "Intake"), "ID", "StatusName");
            return View();
        }

        // POST: Intake/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CaseNumber,DeviceModel,CaseAgent,Custodian,WarrantNumber,StorageLocation,IntakeStatus,ReceivedDate,RequestedByDate,IntakeNote")] Intake intake)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Intakes.Add(intake);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex )
            {
                Console.WriteLine(dex);
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(intake);
        }

        // GET: Intake/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Intake intake = db.Intakes.Find(id);
            if (intake == null)
            {
                return HttpNotFound();
            }
            return View(intake);
        }

        // POST: Intake/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        //Edit([Bind(Include = "ID,CaseNumber,DeviceModel,CaseAgent,Custodian,WarrantNumber,StorageLocation,IntakeStatus,ReceivedDate,RequestedByDate,IntakeNote")] Intake intake)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var intakeToUpdate = db.Intakes.Find(id);
            if (TryUpdateModel(intakeToUpdate, "", new string[] { "CaseNumber","DeviceModel","CaseAgent","Custodian","WarrantNumber","StorageLocation","IntakeStatus","ReceivedDate","RequestedByDate","IntakeNote" }))
                if (ModelState.IsValid)
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
            //db.Entry(intake).State = EntityState.Modified;
            //return View(intake);
            return View(intakeToUpdate);
        }

        // GET: Intake/Delete/5
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
            Intake intake = db.Intakes.Find(id);
            if (intake == null)
            {
                return HttpNotFound();
            }
            return View(intake);
        }

        // POST: Intake/Delete/5
        [HttpPost]
            //, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        //DeleteConfirmed(int id)
        {
            try
            {
                Intake intake = db.Intakes.Find(id);
                db.Intakes.Remove(intake);
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
