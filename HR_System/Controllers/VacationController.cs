using Microsoft.AspNetCore.Mvc;
using HR_System.Models;

namespace HR_System.Controllers
{
    public class VacationController : Controller
    {
        HrSysContext db;
        public VacationController(HrSysContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
	[ValidateAntiForgeryToken]
        public IActionResult Index(Vacation v)
        {
            if (ModelState.IsValid)
            {
                db.Vacations.Add(v);
                db.SaveChanges();
                return RedirectToAction("display");
            }
            else
            {
                return View(v);

            }

        }
        public IActionResult display()
        {
            return View(db.Vacations.ToList());
        }

    }
}
