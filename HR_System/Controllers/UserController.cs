using Microsoft.AspNetCore.Mvc;
using HR_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HR_System.Controllers;
public class UserController : Controller
{
    HrSysContext db;
    public UserController(HrSysContext db)
    {
        this.db = db;
    }

    // List Users
    public IActionResult Index()
    {
        var gId = HttpContext.Session.GetString("groupId");
        if (gId != null)
        {
            string pagename = "User";
            ViewBag.groupId = db.CRUDs.Where(n => n.GroupId == int.Parse(gId) && n.Page.PageName == pagename).FirstOrDefault();
        }
        return View(db.Users.ToList());
    }

    // Add New User 

    public IActionResult addUser()
    {
        // Send Groups Drop Down List Data 
        ViewBag.groups = new SelectList( db.Groups.ToList() , "GroupId", "GroupName");

        return View();
    }

    [HttpPost]
    public IActionResult addUser(User newUser)
    {
        db.Users.Add(newUser);
        db.SaveChanges();
        return RedirectToAction( "Index","User");
    }

    
    // Edit User
    public IActionResult edit(int id)
    {
         User OldUser =db.Users.Find(id);
        ViewBag.groups = new SelectList(db.Groups.ToList(), "GroupId", "GroupName");

        return View(OldUser);
    }

    [HttpPost]
    public IActionResult edit(User newUser)
    {
        User old = db.Users.Find(newUser.UserId);
        old.Username = newUser.Username;
        old.Email = newUser.Email;
        old.GroupId = newUser.GroupId;
        db.SaveChanges();

        return RedirectToAction("Index", "User");
    }

    // Delete User

    public IActionResult delete(int? id)
    {
        if (id != null)
        {
            User u = db.Users.Find(id);
            if(u != null)
            db.Users.Remove(u);
            db.SaveChanges();
        }

        return RedirectToAction("Index", "User");
    }





}
