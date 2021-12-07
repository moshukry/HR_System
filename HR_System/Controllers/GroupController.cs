using HR_System.Models;
using HR_System.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers;
public class GroupController : Controller
{
    HrSysContext db;
    public GroupController(HrSysContext db)
    {
        this.db = db;
    }

    // List Groups
    public IActionResult Index()
    {

        List<PagesVM> Pageslist = new List<PagesVM>();
        foreach (var item in db.Pages.ToList())
        {
            Pageslist.Add(new PagesVM { page = item });
        }



        GroupPageVM model = new GroupPageVM()
        {
            group = new Group(),
            PagesVM = Pageslist
        };

        ViewBag.x = model.PagesVM.Count;
        return View(model);
    }

    [HttpPost]
    public ActionResult Index(GroupPageVM GP)
    {
        Group x = new Group();
        x = GP.group;
        db.Groups.Add(x);
        db.SaveChanges();

        foreach (var item in GP.PagesVM)
        {
            if (item.ADD)
            {
                Crud sks = new Crud();
                sks.GroupId = GP.group.GroupId;
                sks.Group = GP.group;
                sks.Add = true ;
                sks.PageId = item.page.PageId;
                db.CRUDs.Add(sks);
                db.SaveChanges();

            }

            //if (item.checkBox)
            //{
            //    user_skills sks = new user_skills();
            //    sks.id = s.user.id;
            //    sks.user = s.user;
            //    sks.rate = 5;
            //    sks.skill_id = item.skill.skill_id;
            //    db.user_skills.Add(sks);
            //    db.SaveChanges();

            //}

        }

        return RedirectToAction("Index", "User");

    }







    }
