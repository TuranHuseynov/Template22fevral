﻿using System;
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
    public class DesignationsController : Controller
    {
        private newDataEntities db = new newDataEntities();

        // GET: Designations
        public ActionResult Index()
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            var designations = db.Designations.Include(d => d.Departament);
            return View(designations.ToList());
        }

        // GET: Designations/Details/5
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
            Designation designation = db.Designations.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        // GET: Designations/Create
        public ActionResult Create()
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.depart_id = new SelectList(db.Departaments, "id", "depart_name");
            return View();
        }

        // POST: Designations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,desig_name,depart_id")] Designation designation)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Designations.Add(designation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.depart_id = new SelectList(db.Departaments, "id", "depart_name", designation.depart_id);
            return View(designation);
        }

        // GET: Designations/Edit/5
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
            Designation designation = db.Designations.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            ViewBag.depart_id = new SelectList(db.Departaments, "id", "depart_name", designation.depart_id);
            return View(designation);
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,desig_name,depart_id")] Designation designation)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(designation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.depart_id = new SelectList(db.Departaments, "id", "depart_name", designation.depart_id);
            return View(designation);
        }

        // GET: Designations/Delete/5
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
            Designation designation = db.Designations.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        // POST: Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            Designation designation = db.Designations.Find(id);
            db.Designations.Remove(designation);
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
