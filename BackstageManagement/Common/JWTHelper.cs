using BackstageManagement.Model.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackstageManagement.Common;
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
        public static string SetJwtEncode(EmployeeEntity entity, int expire = 15)
        {
            var userInfo = new UserInfo() { exp = CommonHelper.GetTimeStamp ()+ expire }.UpdateInfoByClass<UserInfo, EmployeeEntity>(entity);
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(userInfo, secret);
        }

        /// <summary>
        /// 根据jwtToken获取实体
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static EmployeeEntity GetJwtDecode(string token)
        {
            if (String.IsNullOrEmpty(token)) return null;
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            var userInfo = decoder.DecodeToObject<UserInfo>(token, secret, true);
            return new EmployeeEntity().UpdateInfoByClass<EmployeeEntity, UserInfo>(userInfo);
        }

    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string LoginNo { get; set; }
        public string LoginName { get; set; }
        public int EmployeeType { get; set; }
        public int exp { get; set; }
    }
}