using BackstageManagement.IServices;
using BackstageManagement.Model;
using BackstageManagement.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BackstageManagement.Controllers
{
    public class PermissionController : BaseController
    {
        private readonly IPermissionServices _permissionServices;
        public PermissionController(IRolePermissionServices rolePermissionServices, 
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
        /// 获取权限信息
        /// </summary>
        /// <param name="page">页</param>
        /// <param name="limit">行数</param>
        /// <returns></returns>
        public async Task<ActionResult> GetPermissionInfo(int page, int limit)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                var permissions =await _permissionServices.GetAll();
                foreach (var item in permissions)
                {
                    var permission =await _permissionServices.GetSingle(c=>c.Id== item.ParentId);
                    if (permission != null)
                    {
                        item.ParentName = permission.Name;
                    }
                }
                result.code = ResponseCode.Success;
                result.data = permissions.ToList().Skip((page - 1) * limit).Take(limit).ToList();
                result.count = permissions.Count;
            }
            catch (Exception ex)
            {
                result.code = ResponseCode.Fail;
                result.msg = ex.ToString();
                result.count = 0;
                await _logServices.WriteExceptionLog(LoginUser.Id, "获取权限菜单", ex.ToString());
            }
            return Json(result);
        }
        /// <summary>
        /// 添加权限菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ActionResult> AddPermissionInfo(string param)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                PermissionEntity entity = JsonConvert.DeserializeObject<PermissionEntity>(param);
                entity.ModifyTime = DateTime.Now;
                var res =await _permissionServices.Add(entity);
                await _logServices.WriteSystemLog(LoginUser.Id, "添加权限菜单", string.Format("信息={0}，结果:{1}", param, res));
            }
            catch (Exception ex)
            {
                await _logServices.WriteExceptionLog(LoginUser.Id, "添加权限菜单", ex.ToString());
                result.code = ResponseCode.Fail;
                result.msg = "添加菜单失败，" + ex.ToString();
            }
            return Json(result);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeletePermission(int Id)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                var res =await _permissionServices.DeleteById(Id);
                await _logServices.WriteSystemLog(LoginUser.Id, "删除权限菜单", string.Format("信息={0}，结果:{1}", Id, res));
            }
            catch (Exception ex)
            {
                await _logServices.WriteExceptionLog(LoginUser.Id, "删除权限菜单", "删除菜单:" + ex.ToString());
                result.code = ResponseCode.Fail;
                result.msg = "删除菜单失败，" + ex.ToString();
            }
            return Json(result);
        }
        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ActionResult> EditPermissionInfo(string param)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                var entity = JsonConvert.DeserializeObject<PermissionEntity>(param);
                entity.ModifyTime = DateTime.Now;
                var res =await _permissionServices.Update(entity);
                await _logServices.WriteSystemLog(LoginUser.Id, "编辑权限菜单", string.Format("编辑菜单，信息={0}，结果:{1}", param, res));
            }
            catch (Exception ex)
            {
                await _logServices.WriteExceptionLog(LoginUser.Id, "编辑权限菜单", "编辑菜单:" + ex.ToString());
                result.code = ResponseCode.Fail;
                result.msg = "编辑菜单失败，" + ex.ToString();
            }
            return Json(result);
        }
        /// <summary>
        /// 获取权限名称
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> QueryPermissionInfo()
        {
            JsonResponse result = new JsonResponse();
            try
            {
                var entitys =await _permissionServices.GetAll();
                result.data = entitys;
            }
            catch (Exception ex)
            {
                result.code = ResponseCode.Fail;
                result.msg = "获取权限失败，" + ex.ToString();
                await _logServices.WriteExceptionLog(LoginUser.Id, "获取权限", "编辑菜单:" + ex.ToString());
            }
            return Json(result);
        }
    }
}