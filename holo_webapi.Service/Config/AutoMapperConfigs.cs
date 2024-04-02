using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using holo_webapi.Model.Entitys;
using holo_webapi.Service.Flower.Dto;
//using holo_webapi.Service.User.Dto;

namespace holo_webapi.Service.Config
{
    public class AutoMapperConfigs: Profile
        // Profile 类是 AutoMapper 库中用于配置映射关系的基类。
    {
        /// <summary>
        /// 在构造函数中配置映射关系
        /// </summary>
        public AutoMapperConfigs()
        {
            //使用 CreateMap 方法来定义对象之间的映射关系。

            //从A => B
            CreateMap<holo_webapi.Model.Entitys.Flower, FlowerRes>(); // 奇了怪了，为毛不能简写
            //提供的源类型 Flower 和目标类型 FlowerRes 的定义，它们的属性名称和类型完全一致。
            //在这种情况下，使用 AutoMapper 进行映射可能看起来似乎没有太多意义，因为属性的名称和类型已经匹配。

            //CreateMap<Users, UserRes>(); // 这里映射就有意义了！因为返回给用户的数据不需要那么多，有的数据只是后端在用。

            //CreateMap<RegisterReq, Users>();

            //左边是源类型（Source Type），表示映射的源对象类型。
            //右边是目标类型（Destination Type），表示映射的目标对象类型。
            //将源对象的属性值映射到目标对象的对应属性上。这样可以简化对象转换的过程，提高代码的可读性和可维护性。

        }
    }
}
