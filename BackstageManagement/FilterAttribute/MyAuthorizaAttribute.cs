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
            //var loginUser = httpContext.Session[Utils.SESSION_LOGIN_ADMIN];
            //if (loginUser == null)
            //{
            //    return false;
            //}
            //return true;

            var cookie = httpContext.Request.Cookies[Utils.COOKIE_LOGIN_KEY];//cookie验证
            var authHeader = httpContext.Request.Headers.AllKeys.FirstOrDefault(c => c == "auth");//头部验证

            if (cookie != null || !string.IsNullOrEmpty(authHeader))
            {
                var token = cookie != null ? cookie.Value : httpContext.Request.Headers["auth"];
                var info = JWTHelper.GetJwtDecode(token);
                return info != null;
            }
            //httpContext.Response.StatusCode = 401;
            return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //base.HandleUnauthorizedRequest(filterContext);
            //filterContext.HttpContext.Response.Redirect("/Login/Index");
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectResult("/Login/Index");
        }
        
    }
}