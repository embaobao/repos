using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CTEP.Models;

namespace CTEP.Controllers
{
    public class UsersController : BaseController
    {







        [HttpGet]
        public ActionResult TEST(int ? page,int ? limit)
        {
            return Json(new MsItem(db.Users.ToList()) { } ,JsonRequestBehavior.AllowGet);
        }
        public ActionResult IsMail(string MAIL)
        {
            return HasMail(MAIL) > 0 ? Json(true) : Json(false);

        }

        public ActionResult Login([Bind(Include = "ID,MAIL,PW,C_ROLE,C_STA")] User user) {
            int i = HasMail(user.MAIL);
            if (i>0)
            {
              User u= db.Users.Find(i);
                if (user.PW == u.PW)
                {
                    return Json(u);//返回对象
                }
                else
                {
                    return Json(new User() { ID=-1});//密码不对
                 }
                
            }
            return  Json(new User() { ID = -2 }); //至少一个不对
        }
        [HttpGet]
        public ActionResult Info(int? id)
        {
            
            return Json(db.UserInfoes.Where(x => x.I_UID == id).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult InfoByMail(string mail)
        {
            return Json(db.UserInfoes.Find(HasMail(mail)), JsonRequestBehavior.AllowGet);
        }




        /// <summary>
        /// 注册接口
        /// </summary>
        /// <param name="user">绑定用户对象</param>
        /// <returns>用户对象 id小于0 则登录失败</returns>
        [HttpPost]
        [Obsolete]
        public ActionResult Register([Bind(Include = "ID,MAIL,PW,C_ROLE,C_STA")] User user)
        {
            try
            {

                if (HasMail(user.MAIL) < 0)
                {
                    if (SendMail(SetObject<User, int>(user, "C_STA", 1)))
                    {
                        AddData<User>(SetObject<User, int>(user, "C_STA", 0));
                    }

                }
                else
                {
                    return Json(false);
                }
                //new User() { MAIL = user.MAIL, PW = user.PW, C_ROLE = user.C_ROLE, C_STA = 1 } )

                //SendMail(SetObject<User, int>(user, "C_STA", 1))

            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
            return Json(true);
        }


        [HttpGet]
        public ActionResult AccountActive(string user)
        {
            User u = null;
            ViewBag.Title = "EMB账户激活";
            try
            {
                u = Tools.FromBase64<User>(user);
                u.ID = HasMail(u.MAIL);
                if (ChangeData<User>(u))
                {
                    ViewBag.txt = "EMB-账户:" + u.MAIL + "激活成功！";
                }
                else
                {
                    ViewBag.txt = "EMB-账户激活失败！错误码：" + ":" + u.C_STA;
                }


            }
            catch (Exception ex)
            {
                ViewBag.txt = "EMB-账户激活失败！错误：" + ex.ToString();
            }
            return View();
        }




        // GET: Users
        public ActionResult Index()
        {


            return View(db.Users.ToList());
        }





        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MAIL,PW,C_ROLE,C_STA")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MAIL,PW,C_ROLE,C_STA")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }







        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




    }
}
