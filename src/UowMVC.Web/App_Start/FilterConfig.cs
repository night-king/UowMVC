using UowMVC.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace UowMVC.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
