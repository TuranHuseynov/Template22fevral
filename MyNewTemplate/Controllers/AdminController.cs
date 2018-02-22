using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyNewTemplate.Models;
using MyNewTemplate.ViewModel;

namespace MyNewTemplate.Controllers
{
    public class AdminController : Controller
    {
        newDataEntities db = new newDataEntities();
       
        // GET: Admin
        public ActionResult Index()
        {
            if (!Check_Admin())
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
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

    }
}