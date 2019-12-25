using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackstageManagement.Common
{
    public class CommonHelper
    {
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static int GetTimeStamp()
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (int)Math.Round((DateTime.Now - unixEpoch).TotalSeconds);
        }
    }
}