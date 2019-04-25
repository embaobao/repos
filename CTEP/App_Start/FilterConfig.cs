using System.Web;
using System.Web.Mvc;
using CTEP.Filter;

namespace CTEP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AdminAuthorizeAttribute());
        }
    }
}
