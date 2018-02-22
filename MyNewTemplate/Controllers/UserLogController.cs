using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyNewTemplate.Models;
namespace MyNewTemplate.Controllers
{
    public class UserLogController : Controller
    {
        newDataEntities db = new newDataEntities();
        // GET: UserLog
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult UsrLog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UsrLog(Employee emp)
        {

            Employee logEmp = db.Employees.Where(e => e.emp_email == emp.emp_email).First();
            Session["Emp_email"] = emp.emp_email;
            Session["Emp_password"] = emp.emp_password;
            
            if (logEmp.emp_email == emp.emp_email)
            {
                if (logEmp.emp_password == emp.emp_password)
                {
                    Session["Emp_email"] = logEmp.emp_email;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "UserLog");
        }


    }
}