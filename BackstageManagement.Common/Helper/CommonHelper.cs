using BackstageManagement.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BackstageManagement.Common
{
    public class CommonHelper
    {
        private static string sh_ipurl = "http://pv.sohu.com/cityjson?ie=utf-8";
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static int GetTimeStamp()
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (int)Math.Round((DateTime.Now - unixEpoch).TotalSeconds);
        }

        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            string ip = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) // 服务器， using proxy        
                //得到真实的客户端地址
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + ""; // Return real client IP.

            else//如果没有使用代理服务器或者得不到客户端的ip not using proxy or can't get the Client IP       
                //得到服务端的地址
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] + ""; //While it can't get the Client IP, it will return proxy IP.

            return ip;
        }

        /// <summary>
        /// 根据地址获取资源
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetStrByUrl(string url)
        {
            using (WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 })
                return await webClient.DownloadStringTaskAsync(url);
        }

        /// <summary>
        /// 获取搜狐的接口返回值
        /// </summary>
        /// <returns></returns>
        public static async Task<SouhuIpResponse> GetSouhuIpResponse()
        {
            try
            {
                var str = await GetStrByUrl(sh_ipurl);
                return JsonConvert.DeserializeObject<SouhuIpResponse>(str.Split('=')[1].TrimEnd(';'));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}