using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace holo_webapi.Service.Order.Dto
{
    public class OrderReq //请求数据传输对象
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int FlowerId { get; set; }
    }
}
