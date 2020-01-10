using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BackstageManagement.Common
{
    public class LogHelper
    {

        /// <summary>
        /// ILog变量
        /// </summary>
        private static ILog log;

        /// <summary>
        /// 初始化log4net配置
        /// </summary>
        /// <param name="fileName"></param>
        public static void InitLog4Net(string fileName)
        {   
            var repository = LogManager.CreateRepository("Repository");      
            XmlConfigurator.Configure(repository, new FileInfo(fileName));
            log = LogManager.GetLogger(repository.Name, "log");    
        }

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public static void WriteDebug(string info, Exception ex = null)
        {
            if (log.IsDebugEnabled) log.Debug(info, ex);
        }
       
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public static void WriteInfo(string info, Exception ex = null)
        {
            if (log.IsInfoEnabled)
                log.Info(info, ex);
        }

        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public static void WriteWarn(string info, Exception ex = null)
        {
            if (log.IsWarnEnabled) log.Warn(info, ex);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public static void WriteError(string info, Exception ex)
        {
            if (log.IsErrorEnabled)      
                log.Error(info, ex);
            
        }

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public static void WriteFatal(string info, Exception ex)
        {
            if (log.IsFatalEnabled)
                log.Fatal(info, ex);
        }

    }
}
