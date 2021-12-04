using Microsoft.AspNetCore.Mvc;
using HR_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_System.Controllers
{
    public class AttendanceController : Controller
    {
        private HrSysContext db;
        public AttendanceController(HrSysContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            ViewBag.Emp = new SelectList(db.Employees.ToList(), "EmpId", "EmpName");
            
            return View(db.Att_dep.ToList());
        }

        public IActionResult Delete(int AttId)
        {
            //AttDep s = db.Att_dep.Find(AttId);
            AttDep s = db.Att_dep.Find(AttId);
            db.Att_dep.Remove(s);
            db.SaveChanges();
            return RedirectToAction("Index");
            //user s = db.users.Find(id);
            //db.users.Remove(s);
            //db.SaveChanges();
            //return RedirectToAction("index");
        }
    }
}
