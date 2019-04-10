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
    public class EvalutionFormsController : BaseController
    {

        /// <summary>
        /// 搜索待评价的课堂 POST: Evalu/SearchEvaluForms
        /// </summary>
        /// <param name="key">关键词 评价表的创建者码/评价表的课程名字/老师名</param>
        /// <returns>true查找成功| 但是没有值 false 查找失败 |查找出符合的 EvalutionFormsList</returns>
        [HttpGet]
        public ActionResult SearchForms(string key)
        {
            //查找准备
            IQueryable<EvalutionForm> _F = null;
            // 关键词去空字符
            key = key.Trim();
            //尝试匹配 评价表的创建者码


            //如果key参数为空 或者空字符串则返回查找成功
            if (string.IsNullOrEmpty(key) || key.Length < 1)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            try
            {
                //初始值为-1 用户ID不可能为负值
                int uFormkey = -1;
                //尝试转化为INT 
                if (int.TryParse(key, out uFormkey))
                {
                    //如果转化成功  Formkey 变化了   准备查找表
                    //根据创建者用户ID来查找表
                    _F = db.EvalutionForms.Where(x => x.I_CREATE_UID == uFormkey) as IQueryable<EvalutionForm>;

                }
                else
                {
                    //转化为INT 失败 则尝试匹配评价表的课程 名字或老师名  是否隐藏评价表的发布 评价 0 false 1 true
                    _F = db.EvalutionForms.Where(x => (x.S_CREATE_UName.Contains(key) || x.S_Name.Contains(key)) && x.C_STA == 0) as IQueryable<EvalutionForm>;
                }
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }


            return Json(_F.ToList(),JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Forms() {
            return Json(db.EvalutionForms.ToList(),JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult EvaluForm([Bind(Include = "ID,MAIL,PW,C_ROLE,C_STA")] User user)
        {
            //验证用户
            IQueryable<User> _users = db.Users.Where(x => x.MAIL == user.MAIL && x.PW == user.PW && x.C_ROLE == user.C_ROLE && x.C_STA == 1).Take(1) as IQueryable<User>;
            //存储绑定表
            List<UserBundTable> us = null;
            //存储评价表
            List<EvalutionForm> ef = new List<EvalutionForm>();
            //建立查找评价表准备
            IQueryable<EvalutionForm> _ef = null;

            //如果用户验证通过
            if (_users.Count() > 0)
            {
                //当前提交的用户
                User uu = _users.FirstOrDefault();
                us = new List<UserBundTable>();
                //查出用户所有绑定信息
                IQueryable<UserBundTable> _ui = db.UserBundTables.Where(x => x.I_UID == uu.ID) as IQueryable<UserBundTable>;
                //如果用户有绑定信息
                if (_ui.Count() > 0)
                {
                    //如果角色是评价者用户
                    if (user.C_ROLE == 0)
                    {


                        us = _ui.ToList();
                        //遍历绑定表查找评价
                        foreach (UserBundTable band in us)
                        {
                            //如果绑定类型为 课程 即 评价表ID 绑定？
                            if (band.I_Type == 0)
                            {
                                // 绑定的评价表
                                _ef = db.EvalutionForms.Where(x => x.ID == band.I_BD_ID) as IQueryable<EvalutionForm>;
                                //查找到的绑定表添加到表列表中
                                ef.Add(_ef.FirstOrDefault());
                            }
                            //如果不是ID绑定 是其他 就是班级绑定 那么就搜素评价表 中绑定值
                            else
                            {
                                //否则 则搜索评价表中 绑定值与用户绑定的 课程
                                _ef = db.EvalutionForms.Where(x => x.I_BD_ID == band.I_BD_ID) as IQueryable<EvalutionForm>;
                                //遍历根据绑定值查出的评价表 添加到表列表中
                                foreach (EvalutionForm form in _ef.ToList())
                                {
                                    ef.Add(form);
                                }
                            }
                        }
                    }



                    //如果账户是角色是发起评价者
                    else if (user.C_STA == 1)
                    {
                        //建立查找评价表准备
                        _ef = db.EvalutionForms.Where(x => x.I_CREATE_UID == user.ID) as IQueryable<EvalutionForm>;
                        ef = _ef.ToList();
                    }



                }
            }


          ;


            return Json(ef.Distinct<EvalutionForm>());
        }


        /// <summary>
        /// 获取评价表
        /// </summary>
        /// <param name="id">评价表id</param>
        /// <returns>评价表Json对象</returns>
        [HttpPost]
        public ActionResult Form(int? id)
        {
            IQueryable<EvalutionForm> ListEvalutionForms = db.EvalutionForms.Where(x => x.ID == id) as IQueryable<EvalutionForm>;
            EvalutionForm ef = ListEvalutionForms.FirstOrDefault();
            return Json(ef);
        }








        [HttpPost]
        public ActionResult PutForm([Bind(Include = "ID,S_Desc,S_Name,I_CREATE_UID,I_Week,I_Type,I_BD_ID,I_Course_ID,S_CREATE_UName,TIME_Star_EV,TIME_END_EV,F_Score,C_STA,Icon")] EvalutionForm eForms)
        {
            try
            {
                int FormsNum = EFormsNumForId(eForms.ID);
                if (FormsNum > 0)
                {
                    db.Entry(eForms).State = EntityState.Modified;
                }
                else
                {

                    db.EvalutionForms.Add(eForms);

                    if (CTNumForId(eForms.I_Course_ID) <= 0)
                    {
                        AddData<CourseTemp>(new CourseTemp
                        {
                            S_Desc = eForms.S_Desc,
                            S_Name = eForms.S_Name,
                            C_STA = eForms.C_STA,
                            Icon= eForms.Icon
                        });
                    }

                }


            }
            catch (Exception)
            {

                return Json(false);
            }


            db.SaveChanges();

            return Json(true);

        }










        // GET: EvalutionForms
        public ActionResult Index()
        {
            return View(db.EvalutionForms.ToList());
        }















        // GET: EvalutionForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvalutionForm evalutionForm = db.EvalutionForms.Find(id);
            if (evalutionForm == null)
            {
                return HttpNotFound();
            }
            return View(evalutionForm);
        }

        // GET: EvalutionForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EvalutionForms/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,S_Desc,S_Name,I_CREATE_UID,I_Week,I_Type,I_BD_ID,I_Course_ID,S_CREATE_UName,TIME_Star_EV,TIME_END_EV,F_Score,C_STA,Icon")] EvalutionForm evalutionForm)
        {
            if (ModelState.IsValid)
            {
                db.EvalutionForms.Add(evalutionForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evalutionForm);
        }

        // GET: EvalutionForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvalutionForm evalutionForm = db.EvalutionForms.Find(id);
            if (evalutionForm == null)
            {
                return HttpNotFound();
            }
            return View(evalutionForm);
        }

        // POST: EvalutionForms/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,S_Desc,S_Name,I_CREATE_UID,I_Week,I_Type,I_BD_ID,I_Course_ID,S_CREATE_UName,TIME_Star_EV,TIME_END_EV,F_Score,C_STA,Icon")] EvalutionForm evalutionForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evalutionForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evalutionForm);
        }

        // GET: EvalutionForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvalutionForm evalutionForm = db.EvalutionForms.Find(id);
            if (evalutionForm == null)
            {
                return HttpNotFound();
            }
            return View(evalutionForm);
        }

        // POST: EvalutionForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvalutionForm evalutionForm = db.EvalutionForms.Find(id);
            db.EvalutionForms.Remove(evalutionForm);
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
