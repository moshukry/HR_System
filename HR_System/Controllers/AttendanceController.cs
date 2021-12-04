using Microsoft.AspNetCore.Mvc;
using HR_System.Models;



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
            
            return View(db.Att_dep.ToList());
        }
    }
}
