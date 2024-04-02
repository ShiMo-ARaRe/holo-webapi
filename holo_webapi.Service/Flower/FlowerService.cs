using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using holo_webapi.Common;
using holo_webapi.Model.Entitys;
using holo_webapi.Service.Flower.Dto;

namespace holo_webapi.Service.Flower
{
    public class FlowerService : IFlowerService
    {
        private readonly IMapper _mapper;        //_mapper 是一个 AutoMapper 的映射器对象，用于执行对象之间的映射操作。
        public FlowerService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<FlowerRes> GetFlowers(FlowerReq req) // FlowerReq是请求数据传输对象
        {
            // db是DbContext类中的静态属性
            var res = DbContext.db.Queryable<holo_webapi.Model.Entitys.Flower>().WhereIF(
                req.Id > 0, x => x.Id == req.Id).WhereIF(req.Type > 0, x => x.Type == req.Type).ToList();

            //Queryable<holo_webapi.Model.Entitys.Flower>()创建一个可查询的对象，表示对 Flower 表进行查询操作。
            //这里使用了泛型参数 holo_webapi.Model.Entitys.Flower 来指定查询的实体类型。

            //WhereIF() 是 Sugar ORM 提供的条件判断方法。它接受两个参数：一个布尔表达式和一个表示条件的 Lambda 表达式。
            //如果第一个参数为真，则将第二个参数添加到查询中作为条件；如果为假，那就没有查询条件，返回全部。

            //x => x.Id == req.Id 是一个 Lambda 表达式，表示筛选条件。它比较 Flower 表中的 Id 字段与 req.Id 的值是否相等。

            //ToList() 将查询结果以列表的形式返回。

            //使用 AutoMapper 进行对象映射的方法调用。它接受两个参数：要映射的源对象 res，和目标对象的类型 List<FlowerRes>。
            return _mapper.Map<List<FlowerRes>>(res); // 使用前提：注册Automapper和使用 CreateMap 方法来定义对象之间的映射关系
            // FlowerRes是响应数据传输对象
        }
    }
}
