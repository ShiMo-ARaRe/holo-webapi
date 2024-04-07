using AutoMapper;
using holo_webapi.Service.Order.Dto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using holo_webapi.Common;
using holo_webapi.Model.Entitys; // 这个命名空间里面的内容不知道和什么东西冲突了

namespace holo_webapi.Service.Order
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;   //_mapper 是一个 AutoMapper 的映射器对象，用于执行对象之间的映射操作。
        public OrderService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public bool CreateOrder(OrderReq req, long userId, ref string msg)
        {
            /*  使用数据库上下文 DbContext 查询鲜花表 Flower，并根据传入的 鲜花Id 筛选出第一个匹配的鲜花。
                这里使用了 Queryable 对象的 First 方法，它返回符合条件的第一个实体对象，如果找不到匹配的记录，则返回默认值。*/
            var flower = DbContext.db.Queryable<holo_webapi.Model.Entitys.Flower>().First(p => p.Id == req.FlowerId);
            if (flower == null)
            {
                msg = "商品信息不存在！";
                return false;
            }
            holo_webapi.Model.Entitys.Order order = new holo_webapi.Model.Entitys.Order() // 订单表
            {
                OrderNumber = DateTime.Now.ToString("yyyyMMddHHmmffff"),
                OrderDate = DateTime.Now,
                UserId = userId,
                FlowerId = req.FlowerId,
                Price = flower.Price // 价格
            };
            return DbContext.db.Insertable(order).ExecuteCommand() > 0; // 大于零为真
        }
        public List<OrderRes> GetOrder(long userId) // 传入用户Id，返回该用户的订单信息
        {
            var order = DbContext.db.Queryable<holo_webapi.Model.Entitys.Order>().Where(p => p.UserId == userId).Select(s => new OrderRes
            {
                Id = s.Id,
                OrderNumber = s.OrderNumber,
                Price = s.Price,
                OrderDate = s.OrderDate,
                FlowerTitle = SqlFunc.Subqueryable<holo_webapi.Model.Entitys.Flower>().Where(f => f.Id == s.FlowerId).Select(f => f.Title) // 子查询
            }).OrderBy(p => p.OrderDate, OrderByType.Desc).ToList(); // 根据订单创建日期降序排序
            return order; //返回值是响应数据传输对象列表
        }
    }
}
