using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HR_System.Models;

namespace HR_System.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly HrSysContext _context;

        public EmployeesController(HrSysContext context)
        {
            _context = context;
        }

        // GET: Employees
        public IActionResult Index()
        {
            return View();
        }
        // GET: AllEmployees 
        public IActionResult allEmployees(string search,int show)
        {
            var employees = _context.Employees.Include(e => e.Dept).ToList();
            if (search != null && show != 0)
            {
                var emps = employees.Where(e => e.EmpName.Contains(search)).Take(show);
                return PartialView(emps);
            }
            if(search != null)
            {
                var emps = _context.Employees.Include(e => e.Dept).Where(e => e.EmpName.Contains(search));
                return PartialView( emps);
            }
            if(show != 0)
            {
                return PartialView(employees.Take(show));
            }
            return PartialView( employees.Take(10));
        }
        // GET: Employees/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees
                .Include(e => e.Dept)
                .FirstOrDefault(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        // Remote Validations for Employee BirthDate and HireDate...
        public IActionResult birthdatecheck(DateTime Birthdate)
        {
            DateTime datebefore20 = new DateTime(DateTime.Now.Year-20,DateTime.Now.Month,DateTime.Now.Day);
            var result = DateTime.Compare(Birthdate, datebefore20);
            if (result <= 1) return Json(true);
            else return Json(false);
        }
        public JsonResult hiredatecheck(DateTime Hiredate)
        {
            DateTime companystartdate = new DateTime(2008,1,1);
            var result = DateTime.Compare(Hiredate, companystartdate);
            if (result >= 0) return Json(true);
            else return Json(false);
        }
        //public TimeSpan? attTime;
        //public IActionResult DeptTimeCheck(TimeSpan? AttTime, TimeSpan? DepartureTime)
        //{
        //    if(AttTime != null)
        //    {
        //        attTime = AttTime;
        //        return Json(true);
        //    }
        //    if (DepartureTime != null)
        //    {
        //        if(DepartureTime > attTime)
        //        {
        //            return Json(true);
        //        }
        //        else
        //        {
        //            return Json(false);
        //        }
        //    }
        //    return Json(false);
        //}
        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewBag.Gender = new SelectList(new List<string>() { "Male", "Female" });
            ViewBag.Depts = new SelectList(_context.Departments, "DeptId", "DeptName");
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
        }
            ViewBag.Gender = new SelectList(new List<string>() { "Male", "Female" },employee.Gender);
            ViewBag.Depts = new SelectList(_context.Departments, "DeptId", "DeptName", employee.DeptId);
            return View(employee);
}


        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Gender = new SelectList(new List<string>() { "Male", "Female" },employee.Gender);
            ViewBag.Depts = new SelectList(_context.Departments, "DeptId", "DeptName", employee.DeptId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmpId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Gender = new SelectList(new List<string>() { "Male", "Female" },employee.Gender);
            ViewBag.Depts = new SelectList(_context.Departments, "DeptId", "DeptId", employee.DeptId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        //// POST: Employees/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);
        //    _context.Employees.Remove(employee);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmpId == id);
        }
    }
}
