using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackstageManagement.IServices;
using System.Threading.Tasks;
using BackstageManagement.Model;
using BackstageManagement.Model.Models;

namespace BackstageManagement.Controllers
{
    public class RolePermissionController : BaseController
    {
        private readonly IPermissionServices _permissionServices;
        public RolePermissionController(IRolePermissionServices rolePermissionServices, 
            IPermissionServices permissionServices,
            ILogServices logServices) : base(rolePermissionServices, logServices)
        {
            _permissionServices = permissionServices;
        }
        public async Task<ActionResult> Index()
        {
            await Task.Run(() => { });
            return View();
        }

        /// <summary>
        /// 查询权限
        /// </summary>
        /// <param name="loginNo"></param>
        /// <returns></returns>
        public async Task<ActionResult> QueryPermissionInfoById(int roleId)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                List<TreeEntity> tree = new List<TreeEntity>();
                var Permissions =await _permissionServices.Query(c=>!c.IsDeleted);
                var userPermission =await _rolePermissionServices.QueryByRoleId(roleId);
                foreach (var item in userPermission)
                {
                    foreach (var per in Permissions)
                    {
                        if ((item.PermissionId == per.Id && per.ParentId != null) || (item.PermissionId == per.Id && Permissions.Where(c => c.ParentId == per.Id).Count() == 0))
                        {
                            per.IsChecked = true;
                        }
                    }
                }
                foreach (var parent in Permissions.Where(a => a.ParentId == null))
                {
                    tree.Add(new TreeEntity
                    {
                        id = parent.Id,
                        @checked = parent.IsChecked,
                        title = parent.Name
                    });
                    foreach (var child in Permissions.Where(a => a.ParentId == parent.Id))
                    {
                        tree.Where(c => c.id == parent.Id).First().children.Add(new TreeEntity()
                        {
                            id = child.Id,
                            @checked = child.IsChecked,
                            title = child.Name
                        });
                    }
                }
                result.data = tree;
            }
            catch (Exception ex)
            {
                result.code = ResponseCode.Fail;
                result.msg = ex.ToString();
            }
            return Json(result);
        }
        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="checkedNode"></param>
        /// <returns></returns>
        public async Task<ActionResult> SavePermissionInfoByLoginNo(int roleId, string checkedNode)
        {
            JsonResponse json = new JsonResponse();
            try
            {
                var node = JsonConvert.DeserializeObject<List<TreeEntity>>(checkedNode);
                List<RolePermissionEntity> rolePermissions = new List<RolePermissionEntity>();
                foreach (var item in node)
                {
                    rolePermissions.Add(new RolePermissionEntity()
                    {
                        RoleId = roleId,
                        PermissionId = item.id,
                        CreationTime=DateTime.Now
                    });
                    if (item.children != null && item.children.Count > 0)
                    {
                        foreach (var child in item.children)
                        {
                            rolePermissions.Add(new RolePermissionEntity()
                            {
                                RoleId = roleId,
                                PermissionId = child.id,
                                CreationTime=DateTime.Now
                            });
                        }
                    }
                }
                var res = await _rolePermissionServices.SaveRolePermissions(roleId, rolePermissions);
                await _logServices.WriteSystemLog(LoginUser.Id, "保存权限", string.Format("信息={0}，结果:{1}", checkedNode, res));
            }
            catch (Exception ex)
            {
                await _logServices.WriteExceptionLog(LoginUser.Id, "保存权限", ex.ToString());
                json.code = ResponseCode.Fail;
                json.msg = "保存用户权限失败," + ex.ToString();
                return Json(json);
            }
            return Json(json);
        }
    }
}