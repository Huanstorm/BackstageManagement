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

        public async Task<ActionResult> GetRolesForList() {
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

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ActionResult> AddRole(string param)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                RoleEntity entity = JsonConvert.DeserializeObject<RoleEntity>(param);
                entity.CreationTime = DateTime.Now;
                entity.CreateUserId = LoginUser.Id;
                int roleId = await _roleServices.AddRole(entity);
                if (roleId == -1)
                {
                    result.code = ResponseCode.Fail;
                    result.msg = "该角色名已存在";
                    return Json(result);
                }
                await _logServices.WriteSystemLog(LoginUser.Id, "添加角色", string.Format("信息={0}，结果:{1}", param, roleId));
                return Json(result);
            }
            catch (Exception ex)
            {
                await _logServices.WriteExceptionLog(LoginUser.Id, "添加角色", ex.ToString());
                result.code = ResponseCode.Fail;
                result.msg = "添加角色异常，" + ex.ToString();
                return Json(result);
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeleteRole(int id)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                var entity = await _roleServices.QueryById(id);
                entity.IsDeleted = true;
                var res = _roleServices.Update(entity);
                await _logServices.WriteSystemLog(LoginUser.Id, "删除角色", string.Format("信息={0}，结果:{1}", JsonConvert.SerializeObject(entity), res));
            }
            catch (Exception ex)
            {
                await _logServices.WriteExceptionLog(LoginUser.Id, "删除角色", ex.ToString());
                result.code = ResponseCode.Fail;
                result.msg = "删除角色异常，" + ex.ToString();
            }
            return Json(result);
        }
        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ActionResult> EditRole(string param)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                RoleEntity entity = JsonConvert.DeserializeObject<RoleEntity>(param);
                var user = await _roleServices.GetSingle(c => !c.IsDeleted && c.Name == entity.Name && c.Id != entity.Id);
                if (user != null)
                {
                    result.code = ResponseCode.Fail;
                    result.msg = "该角色已存在";
                    return Json(result);
                }
                user = await _roleServices.QueryById(entity.Id);
                entity.ModifyTime = DateTime.Now;
                entity.CreationTime = user.CreationTime;
                entity.CreateUserId = user.CreateUserId;
                entity.ModifyUserId = LoginUser.Id;
                var res = await _roleServices.Update(entity);
                await _logServices.WriteSystemLog(LoginUser.Id, "编辑角色", string.Format("信息={0}，结果:{1}", param, res));
                if (!res)
                {
                    result.code = ResponseCode.Fail;
                    return Json(result);
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                await _logServices.WriteExceptionLog(LoginUser.Id, "编辑角色", ex.ToString());
                result.code = ResponseCode.Fail;
                result.msg = "编辑角色异常，" + ex.ToString();
                return Json(result);
            }
        }

        public async Task<ActionResult> GetRoles()
        {
            JsonResponse result = new JsonResponse();
            try
            {
                var entities = await _roleServices.Query(c => !c.IsDeleted&&c.IsEnabled);
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