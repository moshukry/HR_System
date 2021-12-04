using Microsoft.AspNetCore.Mvc;
using HR_System.Models;



namespace HR_System.Controllers
{
    public class AttendanceController : Controller
    {
        HrSysContext db;
        public AttendanceController(HrSysContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
