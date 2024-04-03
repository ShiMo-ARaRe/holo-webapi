using AutoMapper;
using holo_webapi.Service.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using holo_webapi.Common;
using holo_webapi.Model.Entitys;
using holo_webapi.Model.Enum;

namespace holo_webapi.Service.User
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;  //_mapper 是一个 AutoMapper 的映射器对象，用于执行对象之间的映射操作。
        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UserRes GetUsers(UserReq req) //进来的参数是请求数据传输对象
        {
            //使用数据库上下文 DbContext 查询用户表 Users，并根据传入的用户名和密码条件筛选出第一个匹配的用户记录。
            //这里使用了 Queryable 对象的 First 方法，它返回符合条件的第一个实体对象，如果找不到匹配的记录，则返回默认值。
            var user = DbContext.db.Queryable<Users>().First(p => p.UserName == req.UserName && p.Password == req.Password);
            if (user != null)
            {
                // 使用前提：注册Automapper和使用 CreateMap 方法来定义对象之间的映射关系
                return _mapper.Map<UserRes>(user); // 映射 从 Users ==> UserRes
            }
            return new UserRes(); // 出去的参数是响应数据传输对象
        }

        public UserRes RegisterUser(RegisterReq req, ref string msg)
        {
            var user = DbContext.db.Queryable<Users>().First(p => p.UserName == req.UserName);
            if (user != null)
            {
                msg = "账号已存在！";
                return _mapper.Map<UserRes>(user); // 映射后就将该用户返回
            }
            else
            {
                try
                {
                    Users users = _mapper.Map<Users>(req); // 先进行映射 从 RegisterReq ==> Users
                    users.CreateTime = DateTime.Now; // 设置注册时间
                    users.UserType = (int)EnumUserType.普通用户; // 设置默认用户级别
                    bool res = DbContext.db.Insertable(users).ExecuteCommand() > 0;
                    //通过调用 DbContext.db.Insertable(users).ExecuteCommand()，将 users 对象插入到数据库中，返回值为插入操作后所影响的行数
                    if (res) // 行数大于0为真
                    {
                        //再进行查询
                        user = DbContext.db.Queryable<Users>().First(p => p.UserName == req.UserName && p.Password == req.Password);
                        return _mapper.Map<UserRes>(user); // 最后再映射并返回结果 从 Users ==> UserRes
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }
            return new UserRes();
        }
    }
}
