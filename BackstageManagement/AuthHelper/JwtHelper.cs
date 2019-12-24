using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackstageManagement.AuthHelper
{
    public class JwtHelper
    {
        public static string IssueJwt(TokenModelJwt tokenModel)
        {
            //string iss= Appsettings
            JwtSecurityTokenHandler
        }
    }

    public class TokenModelJwt
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }
    }
}