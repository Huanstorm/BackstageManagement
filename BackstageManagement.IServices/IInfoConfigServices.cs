using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.IServices
{
    public interface IInfoConfigServices:IBaseServices<InfoConfigEntity>
    {
        Task<int> AddInfoConfig(InfoConfigEntity entity);

        Task<bool> UpdateInfoConfig(InfoConfigEntity entity);
    }
}
