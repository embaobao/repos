using System;
using System.Collections.Generic;
using System.Web.Mvc;
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
        }
    }

    public class AuthorActionFilterAttribute: ActionFilterAttribute
    {
     

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
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