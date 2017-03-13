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
    public class CustodianController : Controller
    {
        private IntakeContext db = new IntakeContext();

        // GET: Custodian
        public ActionResult Index()
        {
            return View(db.Custodians.ToList());
        }

        // GET: Custodian/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Custodian custodian = db.Custodians.Find(id);
            if (custodian == null)
            {
                return HttpNotFound();
            }
            return View(custodian);
        }

        // GET: Custodian/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Custodian/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustodianFlag,LastName,FirstName,Email,phoneNumber,AgencyName")] Custodian custodian)
        {
            if (ModelState.IsValid)
            {
                db.Custodians.Add(custodian);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(custodian);
        }

        // GET: Custodian/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Custodian custodian = db.Custodians.Find(id);
            if (custodian == null)
            {
                return HttpNotFound();
            }
            return View(custodian);
        }

        // POST: Custodian/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CustodianFlag,LastName,FirstName,Email,phoneNumber,AgencyName")] Custodian custodian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(custodian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(custodian);
        }

        // GET: Custodian/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Custodian custodian = db.Custodians.Find(id);
            if (custodian == null)
            {
                return HttpNotFound();
            }
            return View(custodian);
        }

        // POST: Custodian/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Custodian custodian = db.Custodians.Find(id);
            db.Custodians.Remove(custodian);
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
