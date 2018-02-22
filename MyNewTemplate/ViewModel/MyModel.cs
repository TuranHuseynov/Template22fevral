using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyNewTemplate.Models;

namespace MyNewTemplate.ViewModel
{
    
    public class MyModel
    {
        private newDataEntities db = new newDataEntities();


        public List<Employee> _employer { get; set; }
        public List<Attendence> _attend { get; set; }
        public List<Award> _award { get; set; }
        public List<Departament> _department { get; set; }
        public List<Designation> _design { get; set; }
        public List<Gender> _gender { get; set; }
        public List<Holiday> _holiday { get; set; }
        public List<Leave_App> _leave_app { get; set; }
        public List<Leave_status> _leave_status { get; set; }
        public List<Leave_type> _leave_type { get; set; }
        public List<Notice_Board> _notice { get; set; }
    }
}