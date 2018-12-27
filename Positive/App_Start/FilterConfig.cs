using System.Web;
using System.Web.Mvc;

namespace Positive
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute() { Order = 1 });
        }
    }
}
