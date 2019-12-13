using BackstageManagement.FilterAttribute;
using System.Web;
using System.Web.Mvc;

namespace BackstageManagement
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

            filters.Add(new MyAuthorizaAttribute());

            filters.Add(new MyActionFilterAttribute());

            filters.Add(new MyExceptionFilterAttribute());

        }
    }
}
