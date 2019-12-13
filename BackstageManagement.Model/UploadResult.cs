using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackstageManagement.Model
{
    /// <summary>
    /// 上传返回格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UploadResult<T>
    {
        /// <summary>
        /// 返回代码 0表示成功 1表示失败 2表示该编号已存在
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 返回泛型类
        /// </summary>
        public T data { get; set; }
    }
}