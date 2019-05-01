using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using CTEP.Models;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using System.Reflection;
using System.Linq.Expressions;


namespace CTEP.Controllers
{
    public class BaseController : Controller
    {



        /// <summary>
        ///db 上下文对象
        /// </summary>
        public CTEMPEntities db
        {
            get
            {
                CTEMPEntities db = CallContext.GetData("db") as CTEMPEntities;
                if (db == null)
                {
                    db = new CTEMPEntities();
                    CallContext.SetData("db", db);
                }
                return db;
            }
        }
        public CTEMPEntities db2
        {
            get
            {
                CTEMPEntities db2 = null;
                if (HttpContext.Items["db2"] == null)
                {
                    db2 = new CTEMPEntities();
                    HttpContext.Items["db2"] = db2;
                }
                else
                {
                    db2 = HttpContext.Items["db2"] as CTEMPEntities;
                }
                return db2;
            }
        }

        // GET: Base



        /// <summary>
        /// 账户激活邮件
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Obsolete]
        public bool SendMail(User user)
        {
            return new EMail(user.MAIL).Send(new User { MAIL = user.MAIL, PW = user.PW, C_ROLE = user.C_ROLE, C_STA = 1 });
        }

        /// <summary>
        /// 发送验证码邮件
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        [Obsolete]
        public bool SendMail(string mail)
        {
            try
            {
                Session.Add(mail, new EMail(mail).Send());
            }
            catch (Exception)
            {
                throw;
                return false;
            }
            return true;
        }

        public bool AddData<T>(T val)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(val).State = EntityState.Added;
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {
                throw;
                return false;
            }

            return true;

        }
        public bool ChangeData<T>(T val)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    db.Entry(val).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {
                throw;
                return false;

            }

            return true;

        }
        /// <summary>
        /// 邮箱是否存在
        /// </summary>
        /// <param name="mail">mail</param>
        /// <returns>返回邮箱账户对应ID</returns>
        public int HasMail(string mail)
        {
            try
            {
                return db.Users.Where(x => x.MAIL == mail).AsNoTracking().FirstOrDefault().ID;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public bool DeletedData<T>(T val)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(val).State = EntityState.Deleted;
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {
                return false;
            }

            return true;

        }


        public int UserNumForId(int? id)
        {
            int num = 0;

            try
            {
                if (id < 0)
                {
                    return num;
                }
                num = db.Users.Where(x => x.ID == id).Count();
            }
            catch (Exception)
            {
                num = -1;
            }
            return num;
        }



        public T SetObject<T, V>(T oObj, string key, V val) where T : new()
        {
            object obj = null;
            try
            {
                Type t = typeof(T);
                obj = Activator.CreateInstance(t);
                obj = oObj;
                //取得属性 
                PropertyInfo pi = t.GetProperty(key);
                //给属性赋值 
                pi.SetValue(obj, val, null);

            }
            catch (Exception)
            {
                return oObj;
            }
            return (T)obj;
        }



        public int GetInfoesIDByID(int? id)
        {
            try
            {

                return db.UserInfoes.AsNoTracking().Where(x => x.I_UID == id).FirstOrDefault().I_UID;
            }
            catch (Exception)
            {

                return -1;
            }
        }



        /// <summary>
        /// Json 反序列化成对象
        /// </summary>
        /// <typeparam name="T">转化的类型</typeparam>
        /// <param name="JsonString">Json 的 序列化对象</param>
        /// <returns>反序列化成的对象</returns>
        public T ToObject<T>(string JsonString)
        {
            T data = default;
            try
            {
                data = JsonConvert.DeserializeObject<T>(JsonString);
            }
            catch (Exception)
            {
                return data;
            }
            return data;
        }







        /// <summary>
        /// 验证账户信息
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public bool IsUser(User user)
        {
            int i = HasMail(user.MAIL);
            if (i > 0)
            {
                User u = db.Users.Find(i);
                if (user.PW == u.PW)
                {
                    return true;//返回对象
                }
            }
            return false; //至少一个不对
        }


        public int HasBundType(UserBundTable bund)
        {
            try
            {
                int count = db.UserBundTables.AsNoTracking().Where(x => x.I_Type == bund.I_Type && x.I_UID == bund.I_UID).Count();
                if (count > 0)
                {


                    UserBundTable ut = db.UserBundTables.AsNoTracking().Where(x => x.I_Type == bund.I_Type && x.I_UID == bund.I_UID).Take(1).FirstOrDefault();

                    return ut.ID;
                }
                else
                {
                    return -1;

                }
            }

            catch (Exception)
            {

                throw;
            }

        }
        public int HasBund(UserBundTable bund)
        {
            try
            {
                return db.UserBundTables.AsNoTracking().Where(x => x.I_Type == bund.I_Type && x.I_BD_ID == bund.I_BD_ID && x.I_UID == bund.I_UID).FirstOrDefault().ID;
            }
            catch (Exception)
            {
                return -1;
            }
        }




















        /// <summary>
        ///  查询课程模板是否存在
        /// </summary>
        /// <param name="id">课程模板id</param>
        /// <returns>课程模板数量</returns>
        public int CTNumForId(int? id)
        {
            int num = 0;

            try
            {
                if (id < 0)
                {
                    return num;
                }
                num = db.CourseTemps.Where(x => x.ID == id).Count();
            }
            catch (Exception)
            {
                num = -1;
            }
            return num;
        }





        /// <summary>
        /// 查询评价表是否存在
        /// </summary>
        /// <param name="id">评级表ID</param>
        /// <returns>评价表数量</returns>
        public int EFormsNumForId(int? id)
        {

            int num = 0;


            try
            {
                if (id < 0)
                {
                    return num;
                }

                num = db.EvalutionForms.Where(x => x.ID == id).Count();
            }
            catch (Exception)
            {
                num = -1;
            }
            return num;
        }






    }
}