using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.Model
{
    public class TaobaoIpResponse
    {
        //{"code":0,"data":{"ip":"47.104.252.6","country":"中国","area":"","region":"山东","city":"青岛","county":"XX","isp":"阿里云","country_id":"CN","area_id":"","region_id":"370000","city_id":"370200","county_id":"xx","isp_id":"1000323"}}

        /// <summary>
        /// 标时
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 明细
        /// </summary>
        public TaobaoDetial Data { get; set; }

    }
    public class TaobaoDetial
    {
        public string Ip { get; set; }
        /// <summary>
        /// 城市id
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
    }
}
