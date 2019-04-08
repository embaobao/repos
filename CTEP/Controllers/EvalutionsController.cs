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
    public class EvalutionsController : Controller
    {
        private CTEMPEntities db = new CTEMPEntities();

        // GET: Evalutions
        public ActionResult Index()
        {
            return View(db.Evalutions.ToList());
        }

        // GET: Evalutions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evalution evalution = db.Evalutions.Find(id);
            if (evalution == null)
            {
                return HttpNotFound();
            }
            return View(evalution);
        }

        // GET: Evalutions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evalutions/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,S_Desc,I_CREATE_UID,I_EVT_ID,I_Type,I_Course_ID,TIME_CREATE,F_Score,C_STA,F_TeachObj,F_TeachMethod,F_TeachAbility,F_TeachAttitude,F_StudentRelation")] Evalution evalution)
        {
            if (ModelState.IsValid)
            {
                db.Evalutions.Add(evalution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evalution);
        }

        // GET: Evalutions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evalution evalution = db.Evalutions.Find(id);
            if (evalution == null)
            {
                return HttpNotFound();
            }
            return View(evalution);
        }

        // POST: Evalutions/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,S_Desc,I_CREATE_UID,I_EVT_ID,I_Type,I_Course_ID,TIME_CREATE,F_Score,C_STA,F_TeachObj,F_TeachMethod,F_TeachAbility,F_TeachAttitude,F_StudentRelation")] Evalution evalution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evalution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evalution);
        }

        // GET: Evalutions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evalution evalution = db.Evalutions.Find(id);
            if (evalution == null)
            {
                return HttpNotFound();
            }
            return View(evalution);
        }

        // POST: Evalutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evalution evalution = db.Evalutions.Find(id);
            db.Evalutions.Remove(evalution);
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
