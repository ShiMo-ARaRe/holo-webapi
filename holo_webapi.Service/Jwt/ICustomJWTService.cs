using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using holo_webapi.Service.User.Dto;

namespace holo_webapi.Service.Jwt
{
    public interface ICustomJWTService
    {
        //获取token
        string GetToken(UserRes user);
    }
}
