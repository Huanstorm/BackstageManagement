using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.IServices
{
    public interface ILogServices:IBaseServices<LogEntity>
    {
        Task WriteSystemLog(int loginId, string function, string content);
        Task WriteExceptionLog(int loginId, string function, string content);
    }
}
