using HR_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Session;
using RestSharp;

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
                    return RedirectToAction("profileAdmin");
                }
                else if (Request.Cookies["role"] == "user")
                {
                    var cookie = Request.Cookies["id"];
                    int id = int.Parse(cookie.ToString());
                    var user = db.Users.Find(id);
                    HttpContext.Session.SetString("userData", JsonConvert.SerializeObject(user));
                    return RedirectToAction("profileUser");
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
                //login
                if (remberme == true)
                {
                    //    //add cookies
                    //HttpCookie co = new HttpCookie("userdata");
                    //co.Values.Add("userid", s.id.ToString());
                    //co.Values.Add("name", s.name.ToString());
                    //co.Expires = DateTime.Now.AddMonths(2);
                    //Response.Cookies.Add(co);
                    //    


                    CookieOptions opt = new CookieOptions();
                    opt.Expires = DateTime.Now.AddDays(2);
                    Response.Cookies.Append("id", admin.AdminId.ToString(), opt);
                    Response.Cookies.Append("role", "admin", opt);


                }


                //Request.Cookies.Delete("id");
                HttpContext.Session.SetString("userData", JsonConvert.SerializeObject(admin));
                return RedirectToAction("profileAdmin");
            }
            else
            {
                //ViewBag.status = "incorrect email or password ";
                //return View();
                User user = db.Users.Where(n => n.Username == a.AdminName && n.Password == a.AdminPass).FirstOrDefault();
                if (user != null)
                {
                    //login
                    if (remberme == true)
                    {
                        //    //add cookies
                        //    HttpCookie co = new HttpCookie("userdata");
                        //    co.Values.Add("userid", s.id.ToString());
                        //    co.Values.Add("name", s.name.ToString());
                        //    co.Expires = DateTime.Now.AddMonths(2);
                        //    Response.Cookies.Add(co);

                        CookieOptions opt = new CookieOptions();
                        opt.Expires = DateTime.Now.AddDays(2);
                        Response.Cookies.Append("id", user.UserId.ToString(), opt);
                        Response.Cookies.Append("role", "user", opt);

                    }
                    HttpContext.Session.SetString("userData", JsonConvert.SerializeObject(user));
                    return RedirectToAction("profileUser");

                }
                else
                {
                    ViewBag.status = "incorrect email or password ";
                    return View();
                }

            }
        }

        public ActionResult profileAdmin()
        {
            var admin = JsonConvert.DeserializeObject<Admin>(HttpContext.Session.GetString("userData"));
            return View(db.Admins.Find(admin.AdminId));
        }
        public ActionResult profileUser()
        {
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userData"));
            return View(db.Users.Find(user.UserId));
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
}

