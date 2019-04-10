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
    public class EvalutionsController : BaseController
    {




        // GET: Evalutions

        [HttpPost]
        public ActionResult assessment([Bind(Include = "ID,MAIL,PW,C_ROLE,C_STA")] User user)
        {

            if (UserNumForId(user.ID) > 0)
            {
                MsList ms = new MsList();
                if (user.C_ROLE == 0)
                {
                    List<Evalution> evl = db.Evalutions.Where(x => x.I_CREATE_UID == user.ID).ToList();
                    foreach (var i in evl)
                    {
                        EvalutionForm evff = db.EvalutionForms.Find(i.I_EVT_ID);
                        ms.Add(new MsItem(evff.ID, evff.S_Name, db.Evalutions.Where(x => x.I_EVT_ID == evff.ID)));
                    }
                    return Json(ms, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    List<EvalutionForm> evlist = db.EvalutionForms.Where(x => x.I_CREATE_UID == user.ID).ToList();
                    foreach (var i in evlist)
                    {
                        ms.Add(new MsItem(i.ID, i.S_Name, db.Evalutions.Where(x => x.I_EVT_ID == i.ID).ToList()));
                    }
                    return Json(ms, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false);

        }


        /// <summary>
        /// post assessment 提交评论
        /// </summary>
        /// <param name="assessment">评价对象</param>
        /// <returns>ex 错误信息 ，true 评价提交成功 </returns>
        // POST: Evalu/PutAssessment
        [HttpPost]
        public ActionResult PutAssessment([Bind(Include = "ID,S_Desc,I_CREATE_UID,I_EVT_ID,I_Type,I_Course_ID,TIME_CREATE,F_Score,C_STA,F_TeachObj,F_TeachMethod,F_TeachAbility,F_TeachAttitude,F_StudentRelation")] Evalution evalution)
        {
            // 测试队列
            //MsList ms = new MsList();

            //ms.Add(new MsItem("Post过来的数据", assessment)); // 测试完成，数据到达！

            try
            {

                if (ModelState.IsValid)
                {
                    //准备查找评价表 为修改评价表中分值做准备

                    IQueryable<EvalutionForm> _ef = null;
                    //准备查找课程模板表 为修课程模板表中分值做准备
                    IQueryable<CourseTemp> _ct = null;

                    //1）查看是否有记录 用户是都已经评价过此评价表
                    IQueryable<Evalution> _al = db.Evalutions.AsNoTracking().Where(x => x.I_EVT_ID == evalution.I_EVT_ID && x.I_CREATE_UID == evalution.ID) as IQueryable<Evalution>;

                    if (_al.Count() > 0)
                    {//如果记录中有匹配的记录 1>0的  那就修改
                        evalution.ID = _al.FirstOrDefault().ID;
                        db.Entry(evalution).State = EntityState.Modified;

                        //测试数据 有匹配的记录 评价表
                        //ms.Add(new MsItem("有匹配的记录评价表", assessment));
                    }
                    else
                    {//如果记录中没有匹配的记录 0!>0的 
                        db.Evalutions.Add(evalution);
                        //测试数据 没有匹配的记录 评价表
                        //ms.Add(new MsItem("没有匹配的记录的记录评价表", assessment));
                    }



                    //修改评价表中的分值
                    //查找评价表
                    _ef = db.EvalutionForms.Where(x => x.ID == evalution.I_EVT_ID).AsNoTracking().Take(1) as IQueryable<EvalutionForm>;


                    //取出用户评价的评价表对象
                    EvalutionForm ef = _ef.FirstOrDefault();

                    //测试数据查找评价表
                    //ms.Add(new MsItem("测试数据 查找的评价表", ef));

                    //执行操作 存入评价
                    db.SaveChanges();



                    //2） 修改分数  如果不能保存 若没有人评价 则没有评价记录就不用查找了 算分等。。

                    //查找出多少人提交了对ef 此评价表的 评价
                    IQueryable<Evalution> _ae = db.Evalutions.AsNoTracking().Where(x => x.I_EVT_ID == ef.ID) as IQueryable<Evalution>;
                    if (_ae.Count() > 0)
                    {
                        //记录人数
                        int eNum = _ae.Count();

                        //TSET: 记录人数
                        //ms.Add(new MsItem("TSET:提交了对ef 此评价表的人数", eNum));

                        //TSET: 所有给评价表评价的评价记录
                        //ms.Add(new MsItem("TSET:所有给评价表评价的评价记录", _ae.ToList()));
                        //清空从新计算
                        ef.F_Score = 0;
                        //遍历 所有给评价表评价的评价记录
                        foreach (Evalution a in _ae.ToList())
                        {
                            ef.F_Score += a.F_Score;
                        }


                        //取平均
                        ef.F_Score = (ef.F_Score / eNum);
                        //TSET: 取平均
                        //ms.Add(new MsItem("TSET: 评价表取平均", ef.score));

                    }




                    _ct = db.CourseTemps.AsNoTracking().Where(x => x.ID == evalution.I_Course_ID).Take(1) as IQueryable<CourseTemp>;
                    //取出用户评价的 课程模板表对象
                    CourseTemp ct = _ct.FirstOrDefault();

                    //TSET: 取出课程模板表对象
                    //ms.Add(new MsItem("TSET:取出课程模板表对象", ct));

                    //查找出多少人提交了对ed 此对象 评价表的 评价
                    IQueryable<Evalution> _ac = db.Evalutions.AsNoTracking().Where(x => x.I_Course_ID == ct.ID) as IQueryable<Evalution>;
                    //记录人数
                    if (_ct.Count() > 0)
                    {
                        int cNum = _ac.Count();


                        //TSET: cNum
                        //ms.Add(new MsItem("TSET:cNum", cNum));

                        //清空从新计算
                        ct.F_Score = 0;
                        foreach (Evalution a in _ac.ToList())
                        {
                            ct.F_Score += a.F_Score;
                        }
                        //取平均
                        ct.F_Score = (ct.F_Score / cNum);
                        //TSET: 取平均
                        //ms.Add(new MsItem("TSET:ct取平均", ct.CourseScore));
                        //修改课程模板表分数
                        db.Entry(ct).State = EntityState.Modified;
                    }



                    //修改评价表分数
                    db.Entry(ef).State = EntityState.Modified;

                    db.SaveChanges();

                    //return Json(ms);
                }
            }
            catch (Exception ex)
            {

                return Json(ex.ToString());
            }


            return Json(true);
        }
























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
