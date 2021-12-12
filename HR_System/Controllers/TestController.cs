using Microsoft.AspNetCore.Mvc;
using HR_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_System.Controllers
{
    public class TestController : Controller
    {
        HrSysContext db;

        public TestController(HrSysContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
var gId = HttpContext.Session.GetString("groupId");
            string pageName = "General Settings";
            if (gId != null)
            {
                ViewBag.groupId = db.CRUDs.Where(n => n.GroupId == int.Parse(gId) && n.Page.PageName == pageName).FirstOrDefault();
            }
            var setts = db.Settings.FirstOrDefault();
            ViewBag.vac = new SelectList(new List<string>() { "saturday", "sunday", "Monday", "Tuesday", "wednesday", "Thursday", "Friday" });
            if (setts == null)
            {
                Setting s = new Setting()
                {
                    PlusPerhour = 0,
                    MinusPerhour = 0,
                    Dayoff1 = "",
                    Dayoff2 = ""

                };
                return View(s);
            }
            else
            {
                return View(setts);
            }

        }
        [HttpPost]
        public IActionResult Index(Setting s)
        {

            var sett = db.Settings.ToList().Count;
            if (ModelState.IsValid && sett == 0)

            {
                db.Settings.Add(s);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            else if (ModelState.IsValid && sett > 0)
            {

                Setting se = db.Settings.Find(s.SettingId);
                if (se != null)
                {

                    se.PlusPerhour = s.PlusPerhour;
                    se.MinusPerhour = s.MinusPerhour;
                    se.Dayoff1 = s.Dayoff1;
                    se.Dayoff2 = s.Dayoff2;
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
                else
                {
                    return NotFound();
                }


            }
            else
            {
                return View(s);
            }
        }

    }
}
