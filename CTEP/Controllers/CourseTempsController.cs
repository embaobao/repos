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
    public class CourseTempsController : BaseController
    {
       

        // GET: CourseTemps
        public ActionResult Index()
        {
            return View(db.CourseTemps.ToList());
        }

        // GET: CourseTemps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTemp courseTemp = db.CourseTemps.Find(id);
            if (courseTemp == null)
            {
                return HttpNotFound();
            }
            return View(courseTemp);
        }

        // GET: CourseTemps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseTemps/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,S_Desc,S_Name,S_More,F_Score,C_STA,Icon")] CourseTemp courseTemp)
        {
            if (ModelState.IsValid)
            {
                db.CourseTemps.Add(courseTemp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseTemp);
        }

        // GET: CourseTemps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTemp courseTemp = db.CourseTemps.Find(id);
            if (courseTemp == null)
            {
                return HttpNotFound();
            }
            return View(courseTemp);
        }

        // POST: CourseTemps/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,S_Desc,S_Name,S_More,F_Score,C_STA,Icon")] CourseTemp courseTemp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseTemp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseTemp);
        }

        // GET: CourseTemps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTemp courseTemp = db.CourseTemps.Find(id);
            if (courseTemp == null)
            {
                return HttpNotFound();
            }
            return View(courseTemp);
        }

        // POST: CourseTemps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseTemp courseTemp = db.CourseTemps.Find(id);
            db.CourseTemps.Remove(courseTemp);
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
