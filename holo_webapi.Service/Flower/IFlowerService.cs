using holo_webapi.Service.Flower.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace holo_webapi.Service.Flower
{
    public interface IFlowerService // 接口
    {
        List<FlowerRes> GetFlowers(FlowerReq req); // FlowerReq是请求数据传输对象
        // FlowerRes是响应数据传输对象
    }
}
