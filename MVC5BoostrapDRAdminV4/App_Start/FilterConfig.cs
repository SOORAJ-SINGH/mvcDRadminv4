using System.Web;
using System.Web.Mvc;

namespace MVC5BoostrapDRAdminV4
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
