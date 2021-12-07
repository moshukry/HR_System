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
            //var Gid = HttpContext.Session.GetString("groupId");
            //if (Gid != null)
            //{
            //    string pagename = "Attendance";
            //    ViewBag.groupId = db.CRUDs.Where(n => n.GroupId == int.Parse(Gid) && n.PageId == int.Parse(pagename));
            //}
            //ViewBag.Emp = new SelectList(db.Employees.ToList(), "EmpId", "EmpName");

            //if (search != null)
            //{

            //    var att1 = db.Att_dep.Where(n => n.Emp.EmpName.Contains(search));

            //    return View(att1);
            //}

            //return View(db.Att_dep.ToList());



            //var gId = HttpContext.Session.GetString("groupId");
            //if (gId != null)
            //{
            //    ViewBag.groupId = _context.CRUDs.Where(n => n.GroupId == int.Parse(gId) && n.PageId == 1).FirstOrDefault();
            //}

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
            db.Att_dep.ToList();
            if (Search != null && show != 0)
            {
                var deps = db.Att_dep.Where(n => n.Emp.EmpName.Contains(Search)).Take(show);
                return PartialView(deps);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                return PartialView(db.Att_dep.ToList().Take(show));
            }
            if (show != 0)
            {
                return PartialView(db.Att_dep.ToList().Take(show));
            }
            return View();
        }

        //public IActionResult SearchR(string Search)
        //{
        //    //ViewBag.Emp = new SelectList(db.Employees.ToList(), "EmpId", "EmpName");
        //    //List<AttDep> atts = db.Att_dep.OrderBy(n => n.EmpId).ToList();

        //    if (!string.IsNullOrEmpty(Search))
        //    {
        //        var att1 = db.Att_dep.Where(n => n.Emp.EmpName.Contains(Search));
        //        return PartialView(att1);
        //    }
        //    return RedirectToAction("Index");

        //    //if (Search != null)
        //    //{
        //    //	var att1 = db.Att_dep.Where(n => n.Emp.EmpName.Contains(Search));
        //    //	return View(att1);
        //    //}
        //    //return View(db.Att_dep.ToList());
        //}

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
        //public IActionResult Cancel()
        //{
        //	return RedirectToAction("Index");
        //}
    }
}
