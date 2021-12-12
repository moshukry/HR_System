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
            userORAdmin.admin = db.Admins.Find(int.Parse(admin_id));
            return View(userORAdmin);
        }
        else if (user_id != null)
        {
            var gId = HttpContext.Session.GetString("groupId");
            if (gId != null)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                List<Crud> Rules = db.CRUDs.Where(n => n.GroupId == int.Parse(gId)).ToList();
#pragma warning restore CS8604 // Possible null reference argument.
                ViewBag.PagesRules = Rules;
               
            }
            userORAdmin.user = db.Users.Find(int.Parse(user_id));
            return View(userORAdmin);


            
      


        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            var hrSysContext = _context.Att_dep.Include(a => a.Emp);
            return View(await hrSysContext.ToListAsync());
        }

      
    }
}
