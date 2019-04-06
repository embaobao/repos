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

                return false;
            }

            return true;

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



        public T SetObject<T,V>  (T oObj,string key ,V val) where T:new()
        {
            object obj =null;
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

                 return new T() { };
            }
            return (T)obj;
        }


    }
}