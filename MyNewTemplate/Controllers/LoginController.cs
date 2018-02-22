using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyNewTemplate.Models;

namespace MyNewTemplate.Controllers
{
    public class LoginController : Controller
    {
        newDataEntities db = new newDataEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }




        [HttpPost]
        public ActionResult LogIn(FormCollection frm)
        {
            Session["User_email"] = frm["User_email"];
            Session["User_password"] = frm["User_password"];

            if(Session["User_email"].ToString() == "admin@mail.ru")
            {
                if(Session["User_password"].ToString() == "admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                RedirectToAction("Index");
            }

            return View();
        }
      



        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LogIn");
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

        private bool CheckEmp()
        {

            if (Session["Emp_user"] != null)
            {
                ViewBag.message = "Xosgelmisiniz " + Session["Emp_user"];
                return true;
            }
            else
            {
                ViewBag.message = "Basqa sehifeye yonlendirilirsiniz...";
                return false;
            }
        }

    }
}







           