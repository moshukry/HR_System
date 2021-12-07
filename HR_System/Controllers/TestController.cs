using Microsoft.AspNetCore.Mvc;
using HR_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_System.Controllers
{
    public class TestController : Controller
    {
        HrSysContext db;

        public TestController(HrSysContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            ViewBag.vac = new SelectList(new List<string>() {"saturday","sunday","Monday","Tuesday","wednesday","Thursday","Friday"});
            return View();
        }
        [HttpPost]
        public IActionResult Index(Setting s)
        {
            if (ModelState.IsValid)
            {
                db.Settings.Add(s);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.vac = new SelectList(new List<string>() { "saturday", "sunday", "Monday", "Tuesday", "wednesday", "Thursday", "Friday" });
                return View(s);

            }
            
        }
    
    }
}
