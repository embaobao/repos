using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTEP.Filter;

namespace CTEP.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Evalution()
        {
            return View();
        }
        public ActionResult Histroy()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Community()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Admin()
        {
            
            return View();
        }
        public ActionResult Chat()
        {
            return View();
        }
        public ActionResult Adminvail(string id) {
            int i = db.Users.Where(x => x.MAIL == id && x.C_ROLE == 2 && x.C_STA == 1).Count();
            if (i > 0)
            {
                Session.Add("admin", id);
                return View("Admin");
            }
            else {
                return Json("您没有权限！或者错误的输入！");
            }
          
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}