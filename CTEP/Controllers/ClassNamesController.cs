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
    public class ClassNamesController : Controller
    {
        private CTEMPEntities db = new CTEMPEntities();

        // GET: ClassNames
        public ActionResult Index()
        {
            return View(db.ClassNames.ToList());
        }

        // GET: ClassNames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassName className = db.ClassNames.Find(id);
            if (className == null)
            {
                return HttpNotFound();
            }
            return View(className);
        }

        // GET: ClassNames/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassNames/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,S_Name,I_AC_ID")] ClassName className)
        {
            if (ModelState.IsValid)
            {
                db.ClassNames.Add(className);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(className);
        }

        // GET: ClassNames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassName className = db.ClassNames.Find(id);
            if (className == null)
            {
                return HttpNotFound();
            }
            return View(className);
        }

        // POST: ClassNames/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,S_Name,I_AC_ID")] ClassName className)
        {
            if (ModelState.IsValid)
            {
                db.Entry(className).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(className);
        }

        // GET: ClassNames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassName className = db.ClassNames.Find(id);
            if (className == null)
            {
                return HttpNotFound();
            }
            return View(className);
        }

        // POST: ClassNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassName className = db.ClassNames.Find(id);
            db.ClassNames.Remove(className);
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
