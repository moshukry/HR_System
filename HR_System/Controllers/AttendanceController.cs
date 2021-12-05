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
		public IActionResult Index(string search,int date)
		{
			ViewBag.Emp = new SelectList(db.Employees.ToList(), "EmpId", "EmpName");
			ViewBag.search = search;
			if(date > 0)
            {
				return View(db.Att_dep.Where(n => n.Date.Equals(date)));
			}
			if (!string.IsNullOrEmpty(search))
            {
				return View(db.Employees.Where(n => n.EmpName.Contains(search)).OrderBy(n=>n.EmpId));
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
			//ViewBag.Emp = new SelectList(db.Employees, "EmpId", "EmpName");
			return View();
		}

		// POST: AttDeps/Create
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public  IActionResult Create([Bind("AttId,EmpId,Date,Attendance,Departure")]  AttDep attDep)
		{
			ViewData["EmpId"] = new SelectList(db.Employees, "EmpId", "EmpId", attDep.EmpId);
			//ViewBag.AttDep = attDep;
			//if (ModelState.IsValid)
			//{
			db.Add(attDep);
				db.SaveChanges();
				return RedirectToAction("Index");
			//}
			
			//return View(attDep);
		}






		//public IActionResult Create(AttDep a)
		//{
		//    //db.Att_dep.Add(a);
		//    //db.SaveChanges();
		//    return RedirectToAction("Index");
		//}

		//      [HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Create([Bind("AttId,EmpId,Date,Attendance,Departure")] AttDep attDep)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		db.Add(attDep);
		//		await db.SaveChangesAsync();
		//		return RedirectToAction(nameof(Index));
		//	}
		//	ViewData["EmpId"] = new SelectList(db.Employees, "EmpId", "EmpId", attDep.EmpId);
		//	return View(attDep);
		//}
		public IActionResult Cancel()
		{
			return RedirectToAction("Index");
		}
		//<a asp-action="Cancel"  class="btn btn-danger mr-1 icon-trash" >  Cancel</a>
  //   <a asp-action="Create" class="btn btn-success mr-1 icon-note" >  Save</a>
	}
}
