using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyNewTemplate.Models;
using System.IO;

namespace MyNewTemplate.Controllers
{
    public class EmployeeController : Controller
    {

        private newDataEntities db = new newDataEntities();

        // GET: Employee
        public ActionResult Index()
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            var employees = db.Employees.Include(e => e.Departament).Include(e => e.Designation).Include(e => e.Gender);
            return View(employees.ToList());
        }

        // GET: Employee/Details/5
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
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.emp_dep_id = new SelectList(db.Departaments, "id", "depart_name");
            ViewBag.emp_desig_id = new SelectList(db.Designations, "id", "desig_name");
            ViewBag.emp_gender_id = new SelectList(db.Genders, "id", "gender_name");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "emp_photo,emp_resume,emp_offer_letter")] Employee employee, HttpPostedFileBase emp_resume,HttpPostedFileBase emp_photo,HttpPostedFileBase emp_offer_letter)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                if (Path.GetExtension(emp_photo.FileName) == ".jpg" || Path.GetExtension(emp_photo.FileName) == ".png")
                {
                    var file_name2 = Path.GetFileName(emp_photo.FileName);
                    var src2 = Path.Combine(Server.MapPath("/Upload/Image"), file_name2);
                emp_photo.SaveAs(src2);
                }
                if (Path.GetExtension(emp_resume.FileName) == ".pdf" && emp_resume.ContentLength < 8000000)
                {
                    var file_name = Path.GetFileName(emp_resume.FileName);
                    var src = Path.Combine(Server.MapPath("/Upload/Pdf"), file_name);
                    emp_resume.SaveAs(src);    
                }
                if (Path.GetExtension(emp_offer_letter.FileName) == ".pdf" && emp_offer_letter.ContentLength < 8000000)
                {
                    var file_name3 = Path.GetFileName(emp_offer_letter.FileName);
                    var src3 = Path.Combine(Server.MapPath("/Upload/Pdf"), file_name3);
                    emp_resume.SaveAs(src3);
                }

                employee.emp_offer_letter = Path.GetFileName(emp_offer_letter.FileName);
                employee.emp_photo = Path.GetFileName(emp_photo.FileName);
                employee.emp_resume = Path.GetFileName(emp_resume.FileName);
       
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");


            }

            ViewBag.emp_dep_id = new SelectList(db.Departaments, "id", "depart_name", employee.emp_dep_id);
            ViewBag.emp_desig_id = new SelectList(db.Designations, "id", "desig_name", employee.emp_desig_id);
            ViewBag.emp_gender_id = new SelectList(db.Genders, "id", "gender_name", employee.emp_gender_id);
            return View(employee);
        }

        // GET: Employee/Edit/5
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
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_dep_id = new SelectList(db.Departaments, "id", "depart_name", employee.emp_dep_id);
            ViewBag.emp_desig_id = new SelectList(db.Designations, "id", "desig_name", employee.emp_desig_id);
            ViewBag.emp_gender_id = new SelectList(db.Genders, "id", "gender_name", employee.emp_gender_id);
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,emp_fullname,emp_fathername,emp_dateof_birth,emp_gender_id,emp_phone,emp_address,emp_photo,emp_email,emp_password,emp_dep_id,emp_desig_id,emp_date_of_joining,emp_exit_date,emp_work_status,emp_salary,emp_resume,emp_offer_letter,emp_joining_letter,emp_contact_and_argue,emp_ID_proof")] Employee employee)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_dep_id = new SelectList(db.Departaments, "id", "depart_name", employee.emp_dep_id);
            ViewBag.emp_desig_id = new SelectList(db.Designations, "id", "desig_name", employee.emp_desig_id);
            ViewBag.emp_gender_id = new SelectList(db.Genders, "id", "gender_name", employee.emp_gender_id);
            return View(employee);
        }

        // GET: Employee/Delete/5
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
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
