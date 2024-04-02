using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//用于表示 API 返回结果的模型。

namespace holo_webapi.Model
{
    public  class ApiResult
    {
        public bool IsSuccess { get; set; } //表示 API 调用是否成功的布尔值属性。
        public object Result { get; set; } //表示 API 返回的结果数据的对象属性。
        public string Msg { get; set; } //表示 API 返回的消息或错误信息的字符串属性。
    }
}
