using BackstageManagement.Common;
using BackstageManagement.FilterAttribute;
using BackstageManagement.IServices;
using BackstageManagement.Model;
using BackstageManagement.Model.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace BackstageManagement.Controllers
{
    [MyAuthoriza]
    /// <summary>
    /// 基础控制器
    /// </summary>    
    public class BaseController:Controller
    {
        public IRolePermissionServices _rolePermissionServices;
        public ILogServices _logServices;
        public BaseController(IRolePermissionServices rolePermissionServices, ILogServices logServices) {
            _rolePermissionServices = rolePermissionServices;
            _logServices = logServices;
        }
        public string Path { get; set; }
        private SystemUserEntity _loginUser;
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public SystemUserEntity LoginUser
        {
            get
            {
                //_loginUser = Session[Utils.SESSION_LOGIN_ADMIN] as EmployeeEntity;
                _loginUser = JWTHelper.GetJwtDecode(Request.Cookies[Utils.COOKIE_LOGIN_KEY]?.Value);       
                return _loginUser;
            }
            set
            {
                if (value != null)
                {
                    //Session[Utils.SESSION_LOGIN_ADMIN] = value;                   
                    Response.Cookies.Add(new System.Web.HttpCookie(Utils.COOKIE_LOGIN_KEY, JWTHelper.SetJwtEncode(value)));
                }
                else
                {
                    if (Request.Cookies[Utils.COOKIE_LOGIN_KEY] != null)
                    {
                        var cookie = Request.Cookies[Utils.COOKIE_LOGIN_KEY];
                        cookie.Expires.AddDays(-1);
                        Response.AppendCookie(cookie);
                    }
                }
                _loginUser = value;
            }
        }
        /// <summary>
        /// 判断用户是否登录 登录返回true 未登录返回false
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            return this.LoginUser == null ? false : true;
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (!requestContext.HttpContext.Request.IsAjaxRequest())
            {
                if (LoginUser != null)
                {
                    ViewBag.UserName = LoginUser.RealName;
                }
                this.Path = requestContext.HttpContext.Request.FilePath;
                Task.Run(() => SetMenu()).Wait();
            }
        }
        /// <summary>
        /// 设置菜单
        /// </summary>
        private async Task SetMenu()
        {
            if (IsLogin() && !string.IsNullOrEmpty(this.LoginUser.LoginName))
            {
                StringBuilder sb = new StringBuilder();
                var list =await _rolePermissionServices.QueryByRoleId(LoginUser.RoleId.Value);
                list = list.Where(c => c.Permission != null&&c.Permission.Type==PermissionType.Menu).ToList();
                if (list != null)
                {
                    foreach (var parent in list.Where(a => a.Permission.ParentId == null || a.Permission.ParentId == 0))
                    {
                        StringBuilder sbChild = new StringBuilder();
                        sbChild.AppendFormat("<dl class=\"layui-nav-child\">");
                        bool flag = false;
                        bool exists = false;
                        bool parentFlag = false;
                        var childCount = list.Where(a => a.Permission.ParentId == parent.PermissionId).Count();
                        if (childCount == 0)
                        {
                            parentFlag = parent.Permission.Url.ToLower() == this.Path.ToLower();
                        }
                        foreach (var child in list.Where(a => a.Permission.ParentId == parent.PermissionId))
                        {
                            flag = child.Permission.Url.ToLower() == this.Path.ToLower();
                            if (flag)
                            {
                                exists = true;
                            }
                            sbChild.AppendFormat("<dd {0}><a href=\"{1}\">{2}</a></dd>", flag ? "class=\"layui-this\"" : "", string.IsNullOrEmpty(child.Permission.Url) ? "javascript:void(0)" : child.Permission.Url, child.Permission.Name);
                        }
                        sbChild.AppendFormat("</dl>");

                        sb.AppendFormat("<li class=\"layui-nav-item {0}\">", exists ? "layui-nav-itemed" : (parentFlag ? "layui-this" : ""));
                        sb.AppendFormat("<a href=\"{0}\">{1}</a>", childCount != 0 ? "javascript:void(0);" : parent.Permission.Url, parent.Permission.Name);
                        sb.Append(childCount != 0 ? sbChild.ToString() : "");
                        sb.AppendFormat("</li>");
                    }
                }
                ViewBag.MenuItems = sb.ToString();
            }
        }
        
    }
}