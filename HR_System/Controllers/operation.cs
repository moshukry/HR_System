﻿using HR_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Session;


namespace HR_System.Controllers
{
    public class operation : Controller
    {
        HrSysContext db;

        public operation(HrSysContext db)
        {
               this.db = db;
        }
        public IActionResult Index()
        {

            return View();
        }

        // GET: operation
        public IActionResult login()
        {
            //check if cookies file is exist or not
            if (Request.Cookies["id"] != null)
            {

                //Session.Add("userid", Request.Cookies["hrSystem"].Values["userid"]);

                if (Request.Cookies["role"] == "admin")
                {
                    var cookie = Request.Cookies["id"];
                    int id = int.Parse(cookie.ToString());
                    var admin = db.Admins.Find(id);

                    HttpContext.Session.SetString("userData", JsonConvert.SerializeObject(admin));
                    return RedirectToAction("Index" ,"Dashboard");

                }
                else if (Request.Cookies["role"] == "user")
                {
                    var cookie = Request.Cookies["id"];
                    int id = int.Parse(cookie.ToString());
                    var user = db.Users.Find(id);

                    int user_id = user.UserId;
                    HttpContext.Session.SetString("userId", user_id.ToString());
                    int group_id = (int) user.GroupId;
                    HttpContext.Session.SetString("groupId", group_id.ToString());
                    return RedirectToAction("Index", "Dashboard");
                }

            }
            return View();
        }
        [HttpPost]
        public ActionResult login(Admin a, bool remberme)
        {
            Admin admin = db.Admins.Where(n => n.AdminName == a.AdminName && n.AdminPass == a.AdminPass).FirstOrDefault();
            if (admin != null)
            {
                if (remberme == true)
                {
                    CookieOptions opt = new CookieOptions();
                    opt.Expires = DateTime.Now.AddDays(2);
                    Response.Cookies.Append("id", admin.AdminId.ToString(), opt);
                    Response.Cookies.Append("role", "admin", opt);
                }
                HttpContext.Session.SetString("adminId", admin.AdminId.ToString());
                return RedirectToAction("Index", "Dashboard");
            }
            User user = db.Users.Where(n => n.Username == a.AdminName && n.Password == a.AdminPass).FirstOrDefault();
            if (user != null)
            {
                if (remberme == true)
                {
                    CookieOptions opt = new CookieOptions();
                    opt.Expires = DateTime.Now.AddDays(2);
                    Response.Cookies.Append("id", user.UserId.ToString(), opt);
                    Response.Cookies.Append("role", "user", opt);
                }
                int user_id = user.UserId;
                HttpContext.Session.SetString("userId",user_id.ToString());
                int group_id = (int) user.GroupId;
                HttpContext.Session.SetString("groupId",group_id.ToString());

                Crud Cruds = db.CRUDs.Where(n => n.GroupId == group_id).FirstOrDefault();
          

                // HttpContext.Session.SetObjectAsJson("test", Cruds);

                var json = JsonConvert.SerializeObject(Cruds,
                 new JsonSerializerSettings()
                 {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                     });

                HttpContext.Session.SetString("cruds", json);

                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.status = "incorrect email or password ";
            return View();
        }

        public ActionResult profileAdmin()
        {

            var admin_id = HttpContext.Session.GetString("adminId");
            return View(db.Admins.Find(int.Parse(admin_id.ToString())));

        }
        public ActionResult profileUser()
        {
            //var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userData"));
            var user_id = HttpContext.Session.GetString("userId");
            return View(db.Users.Find(int.Parse(user_id.ToString())));
        }

        public ActionResult logout()
        {
            //Session["userData"] = null;
            //HttpCookie c = new HttpCookie();
            //c.Expires = DateTime.Now.AddMonths(-1);
            //Response.Cookies.Add(c);

            HttpContext.Session.Remove("userData");

            //Erase the data in the cookie
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(-1);
            option.Secure = true;
            option.IsEssential = true;
            Response.Cookies.Append("id", string.Empty, option);
            Response.Cookies.Append("role", string.Empty, option);
            //Then delete the cookie
            Response.Cookies.Delete("id");
            Response.Cookies.Delete("role");

            return RedirectToAction("login");

        }
    }

    public static class SessionExtensions
{
  public static void SetObjectAsJson(this ISession session, string key, object value)
   {
     session.SetString(key, JsonConvert.SerializeObject(value));
   }

   public static T GetObjectFromJson<T>(this ISession session, string key)
   {
     var value = session.GetString(key);
     return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
   }
}
}

