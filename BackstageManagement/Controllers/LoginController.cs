using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackstageManagement.IServices;
using System.Threading.Tasks;
using BackstageManagement.Model;

namespace BackstageManagement.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        public readonly ISystemUserServices _employeeServices;
        public LoginController(IRolePermissionServices rolePermissionServices
            , ISystemUserServices employeeServices
            , ILogServices logServices) : base(rolePermissionServices, logServices)
        {
            _employeeServices = employeeServices;
        }

        public async Task<ActionResult> Index()
        {
            await Task.Run(() => { });
            return View();
        }
        /// <summary>
        /// 登录系统
        /// </summary>
        /// <param name="userName">登录名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task<ActionResult> Login(string userName, string password)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                var user = await _employeeServices.GetEmployeeByNo(userName, password);
                if (user != null)
                {
                    this.LoginUser = user;
                    await _logServices.WriteSystemLog(LoginUser.Id, "登录", " 登录成功！");
                    return Json(result);//登录成功
                }

            }
            catch (Exception ex)
            {
                result.code = ResponseCode.Fail;
                result.msg = ex.ToString();
                await _logServices.WriteExceptionLog(LoginUser.Id, "登录", ex.ToString());
            }
            return Json(result);
        }
    }
}