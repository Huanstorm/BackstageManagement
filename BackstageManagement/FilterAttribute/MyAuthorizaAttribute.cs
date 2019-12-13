using BackstageManagement.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackstageManagement.FilterAttribute
{
    public class MyAuthorizaAttribute:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //return base.AuthorizeCore(httpContext);
            var loginUser = httpContext.Session[Utils.SESSION_LOGIN_ADMIN];
            if (loginUser == null)
            {
                return false;
            }
            return true;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //base.HandleUnauthorizedRequest(filterContext);
            filterContext.HttpContext.Response.Redirect("/Login/Index");
        }
        
    }
}