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
    public class Leave_AppController : Controller
    {
        private newDataEntities db = new newDataEntities();

        // GET: Leave_App
        public ActionResult Index()
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            var leave_App = db.Leave_App.Include(l => l.Employee).Include(l => l.Leave_status).Include(l => l.Leave_type);
            return View(leave_App.ToList());
        }

        // GET: Leave_App/Details/5
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
            Leave_App leave_App = db.Leave_App.Find(id);
            if (leave_App == null)
            {
                return HttpNotFound();
            }
            return View(leave_App);
        }

        // GET: Leave_App/Create
        public ActionResult Create()
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.leave_emp_id = new SelectList(db.Employees, "id", "emp_fullname");
            ViewBag.leave_status_id = new SelectList(db.Leave_status, "id", "status_name");
            ViewBag.leave_type_id = new SelectList(db.Leave_type, "id", "type_name");
            return View();
        }

        // POST: Leave_App/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,leave_type_id,leave_emp_id,leave_date,leave_reason,leave_status_id")] Leave_App leave_App)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Leave_App.Add(leave_App);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.leave_emp_id = new SelectList(db.Employees, "id", "emp_fullname", leave_App.leave_emp_id);
            ViewBag.leave_status_id = new SelectList(db.Leave_status, "id", "status_name", leave_App.leave_status_id);
            ViewBag.leave_type_id = new SelectList(db.Leave_type, "id", "type_name", leave_App.leave_type_id);
            return View(leave_App);
        }

        // GET: Leave_App/Edit/5
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
            Leave_App leave_App = db.Leave_App.Find(id);
            if (leave_App == null)
            {
                return HttpNotFound();
            }
            ViewBag.leave_emp_id = new SelectList(db.Employees, "id", "emp_fullname", leave_App.leave_emp_id);
            ViewBag.leave_status_id = new SelectList(db.Leave_status, "id", "status_name", leave_App.leave_status_id);
            ViewBag.leave_type_id = new SelectList(db.Leave_type, "id", "type_name", leave_App.leave_type_id);
            return View(leave_App);
        }

        // POST: Leave_App/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,leave_type_id,leave_emp_id,leave_date,leave_reason,leave_status_id")] Leave_App leave_App)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(leave_App).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.leave_emp_id = new SelectList(db.Employees, "id", "emp_fullname", leave_App.leave_emp_id);
            ViewBag.leave_status_id = new SelectList(db.Leave_status, "id", "status_name", leave_App.leave_status_id);
            ViewBag.leave_type_id = new SelectList(db.Leave_type, "id", "type_name", leave_App.leave_type_id);
            return View(leave_App);
        }

        // GET: Leave_App/Delete/5
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
            Leave_App leave_App = db.Leave_App.Find(id);
            if (leave_App == null)
            {
                return HttpNotFound();
            }
            return View(leave_App);
        }

        // POST: Leave_App/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            Leave_App leave_App = db.Leave_App.Find(id);
            db.Leave_App.Remove(leave_App);
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
