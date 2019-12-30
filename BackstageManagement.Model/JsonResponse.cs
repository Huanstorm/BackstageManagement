using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackstageManagement.Model
{
    /// <summary>
    /// layui Table返回字段统一格式
    /// </summary>
    public class JsonResponse
    {
        /// <summary>
        /// 返回代码 0表示成功 1表示失败
        /// </summary>
        public ResponseCode code { get; set; } = ResponseCode.Success;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 泛型数据
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 重定向地址
        /// </summary>
        public string redirect { get; set; }
    }
    
}