using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using BackstageManagement.IServices;
using System.Threading.Tasks;
using BackstageManagement.Model;
using BackstageManagement.Model.Models;
using BackstageManagement.Common;

namespace BackstageManagement.Controllers
{
    public class UserController : BaseController
    {
        private readonly ISystemUserServices _systemUserServices;

        public UserController(IRolePermissionServices rolePermissionServices, 
            ISystemUserServices systemUserServices,
            ILogServices logServices) : base(rolePermissionServices, logServices)
        {
            _systemUserServices = systemUserServices;
        }
        public async Task<ActionResult> Index()
        {
            await Task.Run(()=> { });
            return View();
        }
        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="page">页</param>
        /// <param name="limit">行数</param>
        /// <returns></returns>
        public async Task<ActionResult> GetUserInfo(int page, int limit)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                var users =await _systemUserServices.QueryUsers();
                result.code = ResponseCode.Success;
                result.data = users.ToList().Skip((page - 1) * limit).Take(limit).ToList();
                result.count = users.Count;
            }
            catch (Exception ex)
            {
                result.code = ResponseCode.Fail;
                result.msg = "查找用户失败" + ex.ToString();
            }
            return Json(result);
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ActionResult> AddUserInfo(string param)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                SystemUserEntity entity = JsonConvert.DeserializeObject<SystemUserEntity>(param);
                entity.CreationTime = DateTime.Now;
                int userId =await _systemUserServices.AddEmployee(entity);
                if (userId == -1)
                {
                    result.code = ResponseCode.Fail;
                    result.msg = "该用户已存在";
                    return Json(result);
                }
                await _logServices.WriteSystemLog(LoginUser.Id, "添加用户", string.Format("信息={0}，结果:{1}", param, userId));
                return Json(result);
            }
            catch (Exception ex)
            {
                await _logServices.WriteExceptionLog(LoginUser.Id, "添加用户", ex.ToString());
                result.code = ResponseCode.Fail;
                result.msg = "添加用户异常，" + ex.ToString();
                return Json(result);
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeleteUser(int id)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                var entity =await _systemUserServices.QueryById(id);
                entity.IsDeleted = true;
                var res = _systemUserServices.Update(entity);
                await _logServices.WriteSystemLog(LoginUser.Id, "删除用户", string.Format("信息={0}，结果:{1}", JsonConvert.SerializeObject(entity), res));
            }
            catch (Exception ex)
            {
                await _logServices.WriteExceptionLog(LoginUser.Id, "删除用户", ex.ToString());
                result.code = ResponseCode.Fail;
                result.msg = "删除用户异常，" + ex.ToString();
            }
            return Json(result);
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ActionResult> EditUserInfo(string param)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                SystemUserEntity entity = JsonConvert.DeserializeObject<SystemUserEntity>(param);
                var user = await _systemUserServices.GetSingle(c=>!c.IsDeleted&&c.LoginName==entity.LoginName&&c.Id!=entity.Id);
                if (user!=null)
                {
                    result.code = ResponseCode.Fail;
                    result.msg = "该用户已存在";
                    return Json(result);
                }
                user = await _systemUserServices.QueryById(entity.Id);
                entity.ModifyTime = DateTime.Now;
                entity.CreationTime = user.CreationTime;
                entity.CreateUserId = user.CreateUserId;
                var res =await  _systemUserServices.Update(entity);
                await _logServices.WriteSystemLog(LoginUser.Id, "编辑用户", string.Format("信息={0}，结果:{1}", param, res));
                if (!res)
                {
                    result.code = ResponseCode.Fail;
                    return Json(result);
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                await _logServices.WriteExceptionLog(LoginUser.Id, "编辑用户", ex.ToString());
                result.code = ResponseCode.Fail;
                result.msg = "编辑用户异常，" + ex.ToString();
                return Json(result);
            }
        }
        
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Exit()
        {
            JsonResponse result = new JsonResponse();
            try
            {
                //Session.Remove(Utils.SESSION_LOGIN_ADMIN);
                this.LoginUser = null;
                result.redirect = "/";
                return Json(result);
            }
            catch (Exception ex)
            {
                result.code = ResponseCode.Fail;
                result.msg = "用户退出登录异常" + ex.ToString();
                await _logServices.WriteExceptionLog(LoginUser.Id, "用户退出", ex.ToString());
            }
            return Json(result);
        }
    }
}