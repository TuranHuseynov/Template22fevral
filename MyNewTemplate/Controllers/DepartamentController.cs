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
    public class DepartamentController : Controller
    {
        private newDataEntities db = new newDataEntities();

        // GET: Departament
        public ActionResult Index()
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Departaments.ToList());
        }

        // GET: Departament/Details/5
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
            Departament departament = db.Departaments.Find(id);
            if (departament == null)
            {
                return HttpNotFound();
            }
            return View(departament);
        }

        // GET: Departament/Create
        public ActionResult Create()
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Departament/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,depart_name")] Departament departament)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Departaments.Add(departament);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departament);
        }

        // GET: Departament/Edit/5
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
            Departament departament = db.Departaments.Find(id);
            if (departament == null)
            {
                return HttpNotFound();
            }
            return View(departament);
        }

        // POST: Departament/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,depart_name")] Departament departament)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(departament).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departament);
        }

        // GET: Departament/Delete/5
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
            Departament departament = db.Departaments.Find(id);
            if (departament == null)
            {
                return HttpNotFound();
            }
            return View(departament);
        }

        // POST: Departament/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            Departament departament = db.Departaments.Find(id);
            db.Departaments.Remove(departament);
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
