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
    public class AttDepsController : Controller
    {
        private readonly HrSysContext _context;

        public AttDepsController(HrSysContext context)
        {
            _context = context;
        }

        // GET: AttDeps
        public async Task<IActionResult> Index()
        {
            var hrSysContext = _context.Att_dep.Include(a => a.Emp);
            return View(await hrSysContext.ToListAsync());
        }

        // GET: AttDeps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attDep = await _context.Att_dep
                .Include(a => a.Emp)
                .FirstOrDefaultAsync(m => m.AttId == id);
            if (attDep == null)
            {
                return NotFound();
            }

            return View(attDep);
        }

        // GET: AttDeps/Create
        public IActionResult Create()
        {
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId");
            return View();
        }

        // POST: AttDeps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttId,EmpId,Date,Attendance,Departure")] AttDep attDep)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attDep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", attDep.EmpId);
            return View(attDep);
        }

        // GET: AttDeps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attDep = await _context.Att_dep.FindAsync(id);
            if (attDep == null)
            {
                return NotFound();
            }
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", attDep.EmpId);
            return View(attDep);
        }

        // POST: AttDeps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttId,EmpId,Date,Attendance,Departure")] AttDep attDep)
        {
            if (id != attDep.AttId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attDep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttDepExists(attDep.AttId))
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
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", attDep.EmpId);
            return View(attDep);
        }

        // GET: AttDeps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attDep = await _context.Att_dep
                .Include(a => a.Emp)
                .FirstOrDefaultAsync(m => m.AttId == id);
            if (attDep == null)
            {
                return NotFound();
            }

            return View(attDep);
        }

        //POST: AttDeps/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var attDep = await _context.Att_dep.FindAsync(id);
        //    _context.Att_dep.Remove(attDep);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool AttDepExists(int id)
        {
            return _context.Att_dep.Any(e => e.AttId == id);
        }
    }
}
