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
    public class InfoConfigController : BaseController
    {
        private readonly IInfoConfigServices _infoConfigServices;
        public InfoConfigController(IEmployeePermissionServices employeePermissionServices,
            IInfoConfigServices infoConfigServices, 
            ILogServices logServices) : base(employeePermissionServices,logServices)
        {
            _infoConfigServices = infoConfigServices;
        }

        // GET: InfoConfig
        public async Task<ActionResult> Index()
        {
            await Task.Run(() => { });
            return View();
        }

        /// <summary>
        /// 从数据库查询配置信息列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="KeyName"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetInfoConfig(int page, int limit)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                List<InfoConfigEntity> infoconfigs =await _infoConfigServices.GetAll(); ;
                result.data = infoconfigs.Skip((page - 1) * limit).Take(limit).ToList();
                result.count = infoconfigs.Count;
            }
            catch (Exception ex)
            {
                result.code = ResponseCode.Fail;
                result.msg = ex.ToString();
            }
            return Json(result);
        }

        /// <summary>
        /// 删除配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeleteInfoConfig(int id)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                var entity = await _infoConfigServices.QueryById(id);
                var res =await _infoConfigServices.DeleteById(id);
                await _logServices.WriteSystemLog(LoginUser.Id, "删除配置信息", string.Format("信息={0}，结果:{1}", JsonConvert.SerializeObject(entity), res));
            }
            catch (Exception ex)
            {
                await _logServices.WriteExceptionLog(LoginUser.Id, "删除配置信息", ex.ToString());
                result.code = ResponseCode.Fail;
                result.msg = "删除配置失败，" + ex.ToString();
            }
            return Json(result);
        }

        /// <summary>
        /// 编辑配置信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ActionResult> EditInfoConfig(string param)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                InfoConfigEntity entity = JsonConvert.DeserializeObject<InfoConfigEntity>(param);
                var old = await _infoConfigServices.QueryById(entity.Id);
                entity.CreationTime = old.CreationTime;
                var res =await _infoConfigServices.UpdateInfoConfig(entity); //更新数据库
                await _logServices.WriteSystemLog(LoginUser.Id, "编辑配置信息", string.Format("信息={0}，结果:{1}", param, res));
            }
            catch (Exception ex)
            {
                await _logServices.WriteExceptionLog(LoginUser.Id, "编辑配置信息", ex.ToString());
                result.code = ResponseCode.Fail;
                result.msg = "编辑配置失败，" + ex.ToString();
            }
            return Json(result);
        }

        /// <summary>
        /// 增加配置信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ActionResult> AddInfoConfig(string param)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                InfoConfigEntity entity = JsonConvert.DeserializeObject<InfoConfigEntity>(param);
                entity.CreationTime = DateTime.Now;
                var res =await _infoConfigServices.AddInfoConfig(entity);
                if (res ==-1)
                {
                    result.code = ResponseCode.Fail;
                    result.msg = "配置信息已存在";
                    return Json(result);
                }
                await _logServices.WriteSystemLog(LoginUser.Id, "添加配置信息", string.Format("信息={0}，结果:{1}", param, res));
            }
            catch (Exception ex)
            {
                await _logServices.WriteExceptionLog(LoginUser.Id, "添加配置信息", ex.ToString());
                result.code = ResponseCode.Fail;
                result.msg = "添加配置信息失败，" + ex.ToString();
            }
            return Json(result);
        }
    }
}