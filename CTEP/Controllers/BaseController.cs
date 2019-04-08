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

        // GET: Base



        [Obsolete]
        public bool SendMail(User user)
        {
            return new EMail(user.MAIL).Send(new User { MAIL = user.MAIL, PW = user.PW, C_ROLE = user.C_ROLE, C_STA = 1 });
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
                ////取得ID字段 
                //FieldInfo fi = t.GetField(key);
                ////给ID字段赋值 
                //fi.SetValue(obj, "k001");

                //取得属性 
                PropertyInfo pi = t.GetProperty(key);


                //给属性赋值 
                pi.SetValue(obj, val, null);
                ////取得show方法 
                //MethodInfo mi = t.GetMethod("show");
                ////调用show方法 
                //mi.Invoke(obj, null);



            }
            catch (Exception)
            {
                return oObj;
            }
            return (T)obj;
        }



        public int GetInfoesIDByID(int ? id) 
        {
            try
            {

                return db.UserInfoes.AsNoTracking().Where(x=>x.I_UID==id).FirstOrDefault().I_UID;
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

    }
}