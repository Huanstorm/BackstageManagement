using BackstageManagement.Model.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackstageManagement
{
    public class JWTHelper
    {
        //私钥
        private static string secret = System.Configuration.ConfigurationManager.AppSettings["jwtSecret"] + "";

        /// <summary>
        /// 生成JwtToken
        /// </summary>
        /// <param name="payload">不敏感的用户数据</param>
        /// <returns></returns>
        public static string SetJwtEncode(SystemUserEntity entity, int exp = 2 * 60 * 60)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var payload = new UserInfo().UpdateInfoByClass<UserInfo, SystemUserEntity>(entity);
            //IDictionary<string, object> dict = new Dictionary<string, object>()
            //{
            //    ////这验证十秒比较困难，JWT有缓冲时间，时间长点就可以验证，所以这边直接使用自己时间校验
            //    { "exp",Common.CommonHelper.GetTimeStamp()+10}
            //};
            payload.exp = Common.CommonHelper.GetTimeStamp() + exp;      
            return encoder.Encode(payload, secret);
        }

        /// <summary>
        /// 根据jwtToken获取实体
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static SystemUserEntity GetJwtDecode(string token)
        {
            try
            {
                if (String.IsNullOrEmpty(token)) return null;
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                var entity = decoder.DecodeToObject<UserInfo>(token, secret, true);
                return entity == null || entity.Id ==0|| string.IsNullOrEmpty(entity.LoginName) || string.IsNullOrEmpty(entity.RealName) || Common.CommonHelper.GetTimeStamp() > entity.exp ? null : new SystemUserEntity().UpdateInfoByClass<SystemUserEntity, UserInfo>(entity);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }

    public class UserInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>

        public string RealName { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>

        public int? RoleId { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int exp { get; set; }


    }
}