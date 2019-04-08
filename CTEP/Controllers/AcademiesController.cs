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
    public class AcademiesController : Controller
    {
        private CTEMPEntities db = new CTEMPEntities();

        // GET: Academies
        public ActionResult Index()
        {
            return View(db.Academies.ToList());
        }

        // GET: Academies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Academy academy = db.Academies.Find(id);
            if (academy == null)
            {
                return HttpNotFound();
            }
            return View(academy);
        }

        // GET: Academies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Academies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,S_Name,I_UN_ID")] Academy academy)
        {
            if (ModelState.IsValid)
            {
                db.Academies.Add(academy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(academy);
        }

        // GET: Academies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Academy academy = db.Academies.Find(id);
            if (academy == null)
            {
                return HttpNotFound();
            }
            return View(academy);
        }

        // POST: Academies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,S_Name,I_UN_ID")] Academy academy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(academy);
        }

        // GET: Academies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Academy academy = db.Academies.Find(id);
            if (academy == null)
            {
                return HttpNotFound();
            }
            return View(academy);
        }

        // POST: Academies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Academy academy = db.Academies.Find(id);
            db.Academies.Remove(academy);
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
