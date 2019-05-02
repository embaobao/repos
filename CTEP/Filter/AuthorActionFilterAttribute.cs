using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CTEP.Models;
namespace CTEP.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public abstract class ActionFilterAttribute : FilterAttribute, IActionFilter, IResultFilter
    {
        protected ActionFilterAttribute() {

        }

        public virtual void OnActionExecuted(ActionExecutedContext filterContext) {

        }
      
        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
          
        }

        public virtual void OnResultExecuted(ResultExecutedContext filterContext)
        {
           
        }

        public virtual void OnResultExecuting(ResultExecutingContext filterContext) {

        }
    }

    public class AdminAuthorizeAttribute : AuthorizeAttribute {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            //string controller1 = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //string action1 = filterContext.ActionDescriptor.ActionName;
            //filterContext.HttpContext.Response.Write("控制器 :"+controller);
            //filterContext.HttpContext.Response.Write("action  :" + action);

            //Index
            //base.OnAuthorization(filterContext);
            //string id = filterContext.HttpContext.Request.QueryString["admin"];
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();
            AppLog.Info(string.Format("请求记录->控制器：{0} 方法 {1}",controller,action));
            List<string> AdminActionslist = new List<string>() { "Index", "Details ", "Create ", "Edit ", "Delete ", "DeleteConfirmed ", "Dispose " };
            List<string> NoneAdminController = new List<string>() { "Base", "Public ", "Home" };

            if (!NoneAdminController.Contains(controller))
            {
                if (AdminActionslist.Contains(action))
                {
                    if (filterContext.HttpContext.Session["admin"] == null)
                    {
                        filterContext.HttpContext. Session.Add("admin", "-1");
                    }
                    string admin = filterContext.HttpContext.Session["admin"].ToString();

                    if (!string.IsNullOrEmpty(admin)&&admin != "-1")
                    {
                        filterContext.HttpContext.Response.Write("<script>alert(' 通过管理员验证 ')</script>");
                    }
                    else {
                       
                        filterContext.HttpContext.Response.Redirect("/Home/Admin");
                        //filterContext.HttpContext.Response.Write("<script>alert('还未通过管理员验证，请输入管理码验证身份！ ')</script>");
                    }
                   
                }
            }

          
        }
    }

    public class AuthorActionFilterAttribute: ActionFilterAttribute
    {
     

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if()  string id = filterContext.HttpContext.Request.QueryString["id"]; filterContext.HttpContext.Response.Write("id:" + id);

            string id = filterContext.HttpContext.Request.QueryString["id"];
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();

            List<string> AdminActionslist = new List<string>() { "Index", "Details ", "Create ", "Edit ", "Delete ", "DeleteConfirmed ", "Dispose " };
            List<string> NoneAdminController = new List<string>() { "Base", "Public ", "Home" };
            
            if (!NoneAdminController.Contains(controller))
            {
                if (AdminActionslist.Contains(action))
                {
                    filterContext.HttpContext.Response.Redirect("/Home/Admin");
                }
            }
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write("在执行动作后");
            base.OnActionExecuted(filterContext);
        }

     
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Write("执行动作前");
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write("执行动作后");
            base.OnResultExecuted(filterContext);
        }

    }
}