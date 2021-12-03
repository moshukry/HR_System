﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HR_System.Models;
//using System.Web.Mvc;

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
        public IActionResult allEmployees(string search)
        {
            if(search != null)
            {
                var emps = _context.Employees.Include(e => e.Dept).Where(e => e.EmpName.Contains(search));
                return PartialView( emps.ToList());
            }
            var employees = _context.Employees.Include(e => e.Dept).ToList();
            return PartialView( employees);
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
        
        public JsonResult daterange(DateTime Birthdate)
        {
            DateTime dt = new DateTime(DateTime.Now.Year-20,DateTime.Now.Month,DateTime.Now.Day);
            var result = DateTime.Compare(dt, Birthdate);
            if (result == 1) return Json(true);
            else return Json(false);
        }
        // GET: Employees/Create
        public IActionResult Create()
        {
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

            var employee = await _context.Employees
                .Include(e => e.Dept)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmpId == id);
        }
    }
}
