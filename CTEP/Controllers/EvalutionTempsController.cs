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
    public class EvalutionTempsController : BaseController
    {
       

        // GET: EvalutionTemps
        public ActionResult Index()
        {
            return View(db.EvalutionTemps.ToList());
        }

        // GET: EvalutionTemps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvalutionTemp evalutionTemp = db.EvalutionTemps.Find(id);
            if (evalutionTemp == null)
            {
                return HttpNotFound();
            }
            return View(evalutionTemp);
        }

        // GET: EvalutionTemps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EvalutionTemps/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,S_Desc,I_CREATE_UID,I_Type,TIME_CREATE,F_Score,C_STA,F_TeachObj,F_TeachMethod,F_TeachAbility,F_TeachAttitude,F_StudentRelation")] EvalutionTemp evalutionTemp)
        {
            if (ModelState.IsValid)
            {
                db.EvalutionTemps.Add(evalutionTemp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evalutionTemp);
        }

        // GET: EvalutionTemps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvalutionTemp evalutionTemp = db.EvalutionTemps.Find(id);
            if (evalutionTemp == null)
            {
                return HttpNotFound();
            }
            return View(evalutionTemp);
        }

        // POST: EvalutionTemps/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,S_Desc,I_CREATE_UID,I_Type,TIME_CREATE,F_Score,C_STA,F_TeachObj,F_TeachMethod,F_TeachAbility,F_TeachAttitude,F_StudentRelation")] EvalutionTemp evalutionTemp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evalutionTemp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evalutionTemp);
        }

        // GET: EvalutionTemps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvalutionTemp evalutionTemp = db.EvalutionTemps.Find(id);
            if (evalutionTemp == null)
            {
                return HttpNotFound();
            }
            return View(evalutionTemp);
        }

        // POST: EvalutionTemps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvalutionTemp evalutionTemp = db.EvalutionTemps.Find(id);
            db.EvalutionTemps.Remove(evalutionTemp);
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
