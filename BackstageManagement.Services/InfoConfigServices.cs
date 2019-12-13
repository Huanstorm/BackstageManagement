using BackstageManagement.IRepository;
using BackstageManagement.IServices;
using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.Services
{
    public class InfoConfigServices:BaseServices<InfoConfigEntity>, IInfoConfigServices
    {
        IInfoConfigRepository _infoConfigRepository;
        public InfoConfigServices(IInfoConfigRepository infoConfigRepository) {
            _infoConfigRepository = infoConfigRepository;
            BaseDal = infoConfigRepository;
        }

        public async Task<int> AddInfoConfig(InfoConfigEntity entity)
        {
            var info =await _infoConfigRepository.GetSingle(c=>c.KeyName==entity.KeyName);
            if (info!=null)
            {
                return -1;
            }
            return await _infoConfigRepository.Add(entity);
        }

        public async Task<bool> UpdateInfoConfig(InfoConfigEntity entity)
        {
            var info = await _infoConfigRepository.GetSingle(c => c.KeyName == entity.KeyName&&c.Id!=entity.Id);
            if (info != null)
            {
                return false;
            }
            return await _infoConfigRepository.Update(entity);
        }
    }
}
