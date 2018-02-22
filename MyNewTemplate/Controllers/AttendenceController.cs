using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyNewTemplate.Models;

namespace MyNewTemplate.Controllers
{
    public class AttendenceController : Controller
    {
        private newDataEntities db = new newDataEntities();

        // GET: Attendence
        public ActionResult Index()
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }

            var attendences = db.Attendences.Include(a => a.Employee).Include(a => a.Leave_type);
            return View(attendences.ToList());
        }

        // GET: Attendence/Details/5
        public ActionResult Details(int? id)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // GET: Attendence/Create
        public ActionResult Create()
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.atten_emp_id = new SelectList(db.Employees, "id", "emp_fullname");
            ViewBag.atten_leave_type_id = new SelectList(db.Leave_type, "id", "type_name");
            return View();
        }

        // POST: Attendence/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,atten_emp_id,atten_status,atten_leave_type_id,atten_date,atten_reason")] Attendence attendence)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                db.Attendences.Add(attendence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.atten_emp_id = new SelectList(db.Employees, "id", "emp_fullname", attendence.atten_emp_id);
            ViewBag.atten_leave_type_id = new SelectList(db.Leave_type, "id", "type_name", attendence.atten_leave_type_id);
            return View(attendence);
        }

        // GET: Attendence/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            ViewBag.atten_emp_id = new SelectList(db.Employees, "id", "emp_fullname", attendence.atten_emp_id);
            ViewBag.atten_leave_type_id = new SelectList(db.Leave_type, "id", "type_name", attendence.atten_leave_type_id);
            return View(attendence);
        }

        // POST: Attendence/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,atten_emp_id,atten_status,atten_leave_type_id,atten_date,atten_reason")] Attendence attendence)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                db.Entry(attendence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.atten_emp_id = new SelectList(db.Employees, "id", "emp_fullname", attendence.atten_emp_id);
            ViewBag.atten_leave_type_id = new SelectList(db.Leave_type, "id", "type_name", attendence.atten_leave_type_id);
            return View(attendence);
        }

        // GET: Attendence/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // POST: Attendence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
              if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }

            Attendence attendence = db.Attendences.Find(id);
            db.Attendences.Remove(attendence);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        private bool Check_Admin()
        {
            if (Session["User_email"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }

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
