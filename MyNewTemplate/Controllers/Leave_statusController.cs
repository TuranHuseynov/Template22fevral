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
    public class Leave_statusController : Controller
    {
        private newDataEntities db = new newDataEntities();

        // GET: Leave_status
        public ActionResult Index()
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Leave_status.ToList());
        }

        // GET: Leave_status/Details/5
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
            Leave_status leave_status = db.Leave_status.Find(id);
            if (leave_status == null)
            {
                return HttpNotFound();
            }
            return View(leave_status);
        }

        // GET: Leave_status/Create
        public ActionResult Create()
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Leave_status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,status_name")] Leave_status leave_status)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Leave_status.Add(leave_status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leave_status);
        }

        // GET: Leave_status/Edit/5
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
            Leave_status leave_status = db.Leave_status.Find(id);
            if (leave_status == null)
            {
                return HttpNotFound();
            }
            return View(leave_status);
        }

        // POST: Leave_status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,status_name")] Leave_status leave_status)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(leave_status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leave_status);
        }

        // GET: Leave_status/Delete/5
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
            Leave_status leave_status = db.Leave_status.Find(id);
            if (leave_status == null)
            {
                return HttpNotFound();
            }
            return View(leave_status);
        }

        // POST: Leave_status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            Leave_status leave_status = db.Leave_status.Find(id);
            db.Leave_status.Remove(leave_status);
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
