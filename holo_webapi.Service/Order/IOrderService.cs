using holo_webapi.Service.Order.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace holo_webapi.Service.Order
{
    public interface IOrderService
    {
        bool CreateOrder(OrderReq req, long userId, ref string msg); // 创建订单
        List<OrderRes> GetOrder(long userId); // 获取订单列表
    }
}
