using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyNewTemplate.Models;
using MyNewTemplate.ViewModel;

namespace MyNewTemplate.Controllers
{
    public class HomeController : Controller
    {
        newDataEntities db = new newDataEntities();
        MyModel vm = new MyModel();
        public ActionResult Index()
        {
            vm._employer = db.Employees.ToList();
            vm._gender = db.Genders.ToList();
            vm._department = db.Departaments.ToList();
            vm._leave_status = db.Leave_status.ToList();
            vm._leave_type = db.Leave_type.ToList();
            vm._leave_app = db.Leave_App.ToList();
            vm._award = db.Awards.ToList();
            vm._attend = db.Attendences.ToList();
            vm._notice = db.Notice_Board.ToList();
            return View(vm);
        }


        public ActionResult Leave()
        {
            return View();
        }




      
    }
}