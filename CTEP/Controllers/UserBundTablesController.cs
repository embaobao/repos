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
    public class UserBundTablesController : BaseController
    {



        // POST: UserBundTables/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]

        public ActionResult AddBund(string bs)
        {
            try
            {
                List<UserBundTable> BsList = ToObject<List<UserBundTable>>(bs);

                foreach (UserBundTable i in BsList)
                {

                    if (i.I_BD_ID >= 0)
                    {


                        if (i.I_Type > 0)
                        {
                            int id = HasBundType(i);


                            if (id > 0)
                            {
                                ChangeData<UserBundTable>(new UserBundTable { ID = id, I_BD_ID = i.I_BD_ID, I_Type = i.I_Type, I_UID = i.I_UID });
                            }
                            else
                            {
                               AddData<UserBundTable>(new UserBundTable{ ID = id,I_BD_ID = i.I_BD_ID, I_Type = i.I_Type, I_UID = i.I_UID });
                            }
                        }

                        else
                        {
                            int id = HasBund(i);
                            if (id > 0)
                            {
                                Json(false);
                            }
                            else
                            {
                                db.UserBundTables.Add(new UserBundTable { ID = id, I_BD_ID = i.I_BD_ID, I_Type = i.I_Type, I_UID = i.I_UID });
                            }

                        }
                    }

                }
              

            }
            catch (Exception)
            {

                throw;
            }
            return Json(true);
        }

        public ActionResult Bund([Bind(Include = "ID,MAIL,PW,C_ROLE,C_STA")] User user)
        {
            if (IsUser(user))
            {
                return Json(db.UserBundTables.Where(x => x.I_UID == user.ID).ToList());
            }
            else
            {
                return Json(false);
            }
        }
















        // GET: UserBundTables
        public ActionResult Index()
        {
            return View(db.UserBundTables.ToList());
        }

        // GET: UserBundTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBundTable userBundTable = db.UserBundTables.Find(id);
            if (userBundTable == null)
            {
                return HttpNotFound();
            }
            return View(userBundTable);
        }

        // GET: UserBundTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserBundTables/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,I_UID,I_Type,I_BD_ID")] UserBundTable userBundTable)
        {
            if (ModelState.IsValid)
            {
                db.UserBundTables.Add(userBundTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userBundTable);
        }

        // GET: UserBundTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBundTable userBundTable = db.UserBundTables.Find(id);
            if (userBundTable == null)
            {
                return HttpNotFound();
            }
            return View(userBundTable);
        }

        // POST: UserBundTables/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,I_UID,I_Type,I_BD_ID")] UserBundTable userBundTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userBundTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userBundTable);
        }

        // GET: UserBundTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBundTable userBundTable = db.UserBundTables.Find(id);
            if (userBundTable == null)
            {
                return HttpNotFound();
            }
            return View(userBundTable);
        }

        // POST: UserBundTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserBundTable userBundTable = db.UserBundTables.Find(id);
            db.UserBundTables.Remove(userBundTable);
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
