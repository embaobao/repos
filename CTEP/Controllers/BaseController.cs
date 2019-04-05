using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using CTEP.Models;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

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
        public ActionResult Index()
        {
            return View();
        }




    }
}