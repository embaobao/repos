using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTEP.Models;

namespace CTEP.Controllers
{
    public class PublicController : BaseController
    {

       
        [HttpPost]
        public ActionResult School(string uName)
        {

            //如果参数为空 或者空字符串则返回 所有列表学校
            if (string.IsNullOrEmpty(uName) || uName.Trim().Length < 1)
            {
                return Json(db.Universities.ToList());
            }

            //如果不为空则 模糊查询
            IQueryable<University> _un = db.Universities.Where(x => x.S_Name.Contains(uName.Trim())) as IQueryable<University>;

            return Json(_un.ToList());
        }




        [HttpPost]
        public ActionResult Academy(string uName, int? uId)
        {
            //查找准备
            IQueryable<Academy> _un = null;
            //如果参数为空 或者空字符串则返回 学校所对应的所有学院列表
            if (string.IsNullOrEmpty(uName) || uName.Trim().Length < 1)
            {
                return Json(db.Academies.Where(x => x.I_UN_ID == uId) as IQueryable<Academy>);
            }
            //如果不为空则 模糊查询
            _un = db.Academies.Where(x => x.S_Name.Contains(uName.Trim()) && x.I_UN_ID == uId) as IQueryable<Academy>;
            return Json(_un.ToList());
        }


        [HttpPost]
        public ActionResult Grad(string uName, int? uId)
        {
            //查找准备
            IQueryable<Grade> _un = null;
            //如果参数为空 或者空字符串则返回 学校所对应的所有年级列表
            if (string.IsNullOrEmpty(uName) || uName.Trim().Length < 1)
            {
                return Json(db.Grades.Where(x => x.I_UN_ID == uId) as IQueryable<Grade>);
            }

            //如果不为空则 模糊查询
            _un = db.Grades.Where(x => x.S_Name.Contains(uName.Trim()) && x.I_UN_ID == uId) as IQueryable<Grade>;

            return Json(_un.ToList());
        }

        //获取班级
        [HttpPost]
        public ActionResult ClassNum(string uName, int? uId)
        {
            //查找准备
            IQueryable<ClassName> _un = null;
            //如果参数为空 或者空字符串则返回 年级所对应的所有班级列表
            if (string.IsNullOrEmpty(uName) || uName.Trim().Length < 1)
            {
                return Json(db.ClassNames.Where(x => x.I_AC_ID == uId) as IQueryable<ClassName>);
            }

            //如果不为空则 模糊查询
            _un = db.ClassNames.Where(x => x.S_Name.Contains(uName.Trim()) && x.I_AC_ID == uId) as IQueryable<ClassName>;

            return Json(_un.ToList());
        }


    }
}