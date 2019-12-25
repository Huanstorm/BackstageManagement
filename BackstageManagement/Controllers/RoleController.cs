using BackstageManagement.IServices;
using BackstageManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BackstageManagement.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRoleServices _roleServices;
        public RoleController(IRolePermissionServices rolePermissionServices, 
            ILogServices logServices,
            IRoleServices roleServices) : base(rolePermissionServices, logServices)
        {
            _roleServices = roleServices;
        }

        public async Task<ActionResult> Index() {
            await Task.Run(() => { });
            return View();
        }

        public async Task<ActionResult> GetRoles() {
            JsonResponse result = new JsonResponse();
            try
            {
                
                var entities = await _roleServices.Query(c => !c.IsDeleted);
                result.data = entities;
            }
            catch (Exception ex)
            {
                result.code = ResponseCode.Fail;
                result.msg = "获取角色信息异常，" + ex.ToString();
                await _logServices.WriteExceptionLog(LoginUser.Id, "获取角色信息", ex.ToString());
            }
            return Json(result);
        }
    }
}