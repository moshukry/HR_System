﻿using Microsoft.AspNetCore.Mvc;
using HR_System.Models;
using HR_System.ViewModels;

namespace HR_System.Controllers;
public class DashboardController : Controller
{
    HrSysContext db;

    public DashboardController(HrSysContext db)
    {
        this.db = db;
    }
    public IActionResult Index()
    {
        var admin_id = HttpContext.Session.GetString("adminId");
        var user_id =  HttpContext.Session.GetString("userId");
        UserORAdmin userORAdmin = new UserORAdmin();

        if (admin_id != null)
        {
            userORAdmin.admin = db.Admins.Find(int.Parse( admin_id));
            ViewBag.PagesRules = null;
            return View(userORAdmin);
        }
        else if (user_id != null)
        {
            var gId = HttpContext.Session.GetString("groupId");
            if (gId != null)
            {

                List<Crud> Rules = db.CRUDs.Where(n => n.GroupId == int.Parse(gId)).ToList();
                ViewBag.PagesRules = Rules;

            }
            userORAdmin.user = db.Users.Find(int.Parse(user_id));
            return View(userORAdmin);


        }
        return NotFound();
    }
}
