using holo_webapi.Service.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using holo_webapi.Model.Entitys;

namespace holo_webapi.Service.User
{
    public interface IUserService
    {
        UserRes GetUsers(UserReq req); // 从 请求数据传输对象==>响应数据传输对象
        UserRes RegisterUser(RegisterReq req, ref string msg); // 从 请求数据传输对象==>响应数据传输对象
    }
}
