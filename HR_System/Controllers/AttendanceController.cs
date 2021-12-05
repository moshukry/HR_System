using Microsoft.AspNetCore.Mvc;
using HR_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Controllers
{
	public class AttendanceController : Controller
	{
		private HrSysContext db;
		public AttendanceController(HrSysContext db)
		{
			this.db = db;
		}
		public IActionResult Index(string? search)
		{
			ViewBag.Emp = new SelectList(db.Employees.ToList(), "EmpId", "EmpName");
			
			if (search != null)
			{
				
				var att1 = db.Att_dep.Where(n=>n.Emp.EmpName.Contains(search));
				
				return View(att1);
			}

			return View(db.Att_dep.ToList());
		}
		public ActionResult Delete(int? id)
        {
            if (id != null)
            {
				AttDep? a = db.Att_dep.Where(n=>n.AttId==id).FirstOrDefault();
                if (a != null)
                {
					db.Att_dep.Remove(a);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
				return NotFound();
			}
			return NotFound();
			
		}

		// GET: AttDeps/Create
		public IActionResult Create()
		{
			ViewData["EmpId"] = new SelectList(db.Employees, "EmpId", "EmpName");
			return View();
		}

		// POST: AttDeps/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public  IActionResult Create([Bind("AttId,EmpId,Date,Attendance,Departure")]  AttDep attDep)
		{
			ViewData["EmpId"] = new SelectList(db.Employees, "EmpId", "EmpName", attDep.EmpId);
            //if (ModelState.IsValid)
            //{
                db.Add(attDep);
				db.SaveChanges();
				return RedirectToAction("Index");
            //}
            //return View(attDep);
        }
		public IActionResult Cancel()
		{
			return RedirectToAction("Index");
		}
	}
}
