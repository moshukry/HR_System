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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult list(string Search, int show)
        {
            var Gid = HttpContext.Session.GetString("groupId");
            if (Gid != null)
            {
                string pagename = "Attendance";
                ViewBag.groupId = db.CRUDs.Where(n => n.GroupId == int.Parse(Gid) && n.PageId == int.Parse(pagename));
            }

            //AttDep a = (AttDep)db.Att_dep.Where(n => n.Emp.EmpName.Contains(Search));
            //db.Att_dep.ToList();
            if (String.IsNullOrEmpty(Search) && show != 0)
            {
                return PartialView(db.Att_dep.ToList().Take(show));
            }
            if (Search != null && show != 0)
            {
                var deps = db.Att_dep.Where(n => n.Emp.EmpName.Contains(Search)).Take(show).ToList();
                return PartialView(deps);
            }
            return PartialView(db.Att_dep.ToList().Take(10));
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                AttDep? a = db.Att_dep.Where(n => n.AttId == id).FirstOrDefault();
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
            ViewBag.EmpId = new SelectList(db.Employees, "EmpId", "EmpName");
            return View();
        }

        // POST: AttDeps/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AttId,EmpId,Date,Attendance,Departure")] AttDep attDep)
        {
            ViewBag.EmpId = new SelectList(db.Employees, "EmpId", "EmpName", attDep.EmpId);
            if (ModelState.IsValid)
            {
                db.Add(attDep);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attDep);
        }
    }
}
