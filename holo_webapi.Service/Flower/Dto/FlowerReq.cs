//Dto 是指数据传输对象（Data Transfer Object），用于在不同层或组件之间传输数据。
//FlowerReq 类：这个类是一个用于表示“Flower”对象的请求数据传输对象。

//这些数据传输对象（Dto）通常用于解耦前端和后端之间的数据传输，以及在不同的层或组件之间传递数据。
//它们可以帮助规范数据的结构和格式，并提供一种统一的方式来传递数据，以确保数据的一致性和可靠性。
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace holo_webapi.Service.Flower.Dto
{
    public class FlowerReq
    {
        public int Id { get; set; }
        public int Type { get; set; }
    }
}
