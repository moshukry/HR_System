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
    public class DashboardController : Controller
    {
        private readonly HrSysContext _context;

        public DashboardController(HrSysContext context)
        {
            _context = context;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            var hrSysContext = _context.Att_dep.Include(a => a.Emp);
            return View(await hrSysContext.ToListAsync());
        }

      
    }
}
