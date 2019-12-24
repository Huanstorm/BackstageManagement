using BackstageManagement.Common;
using BackstageManagement.IServices;
using BackstageManagement.Model.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace BackstageManagement.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    public class BaseController:Controller
    {
        public IEmployeePermissionServices _employeePermissionServices;
        public ILogServices _logServices;
        public BaseController(IEmployeePermissionServices employeePermissionServices,ILogServices logServices) {
            _employeePermissionServices = employeePermissionServices;
            _logServices = logServices;
        }
        public string Path { get; set; }
        private EmployeeEntity _loginUser;
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public EmployeeEntity LoginUser
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
                    Request.Cookies.Add(new System.Web.HttpCookie(Utils.COOKIE_LOGIN_KEY, JWTHelper.SetJwtEncode(value)));
                }
                else
                {
                    if (Request.Cookies[Utils.COOKIE_LOGIN_KEY] != null) Request.Cookies[Utils.COOKIE_LOGIN_KEY].Expires = DateTime.Now.AddDays(-1);
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
                    ViewBag.UserName = LoginUser.LoginName;
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
            if (IsLogin() && !string.IsNullOrEmpty(this.LoginUser.LoginNo))
            {
                StringBuilder sb = new StringBuilder();
                var list =await _employeePermissionServices.QueryByEmployeeId(LoginUser.Id);
                list = list.Where(c => c.Permission != null).ToList(); ;
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
                            parentFlag = parent.Permission.PermissionUrl.ToLower() == this.Path.ToLower();
                        }
                        foreach (var child in list.Where(a => a.Permission.ParentId == parent.PermissionId))
                        {
                            flag = child.Permission.PermissionUrl.ToLower() == this.Path.ToLower();
                            if (flag)
                            {
                                exists = true;
                            }
                            sbChild.AppendFormat("<dd {0}><a href=\"{1}\">{2}</a></dd>", flag ? "class=\"layui-this\"" : "", string.IsNullOrEmpty(child.Permission.PermissionUrl) ? "javascript:void(0)" : child.Permission.PermissionUrl, child.Permission.PermissionName);
                        }
                        sbChild.AppendFormat("</dl>");

                        sb.AppendFormat("<li class=\"layui-nav-item {0}\">", exists ? "layui-nav-itemed" : (parentFlag ? "layui-this" : ""));
                        sb.AppendFormat("<a href=\"{0}\">{1}</a>", childCount != 0 ? "javascript:void(0);" : parent.Permission.PermissionUrl, parent.Permission.PermissionName);
                        sb.Append(childCount != 0 ? sbChild.ToString() : "");
                        sb.AppendFormat("</li>");
                    }
                }
                ViewBag.MenuItems = sb.ToString();
            }
        }
        
    }
}