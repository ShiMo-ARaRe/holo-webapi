using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using holo_webapi.Model;
using holo_webapi.Service.User.Dto;
using System.Security.Cryptography;

namespace holo_webapi.Service.Jwt
{
    public class CustomJWTService : ICustomJWTService
    {
        //JWTTokenOptions 是一个我们自己定义的，表示 JWT 选项的类，其中包含了 JWT 的受众、安全密钥和签发者等属性。
        private readonly JWTTokenOptions _JWTTokenOptions;
        /// <summary>
        ///  获取 JWT 配置
        /// </summary>
        /// <param name="jwtTokenOptions"></param>
        public CustomJWTService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        /*IOptionsMonitor<T> 是 ASP.NET Core 中用于获取配置选项的接口，它可以监视配置选项的更改。
            在这里，IOptionsMonitor<JWTTokenOptions> 是用于获取 JWT 配置选项的实例。
            详细配置见appsettings.json文件*/
        {
            _JWTTokenOptions = jwtTokenOptions.CurrentValue;
            //必须注册JWT才能使用，不然下面打印均为空
            Console.WriteLine($"{_JWTTokenOptions.Audience}--{_JWTTokenOptions.SecurityKey}--{_JWTTokenOptions.Issuer}");
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetToken(UserRes user)
        {
            #region 有效载荷，大家可以自己写，爱写多少写多少；尽量避免敏感信息
            var claims = new[]
            {
                new Claim("Id",user.Id.ToString()),
                new Claim("NickName",user.NickName),
                new Claim("UserName",user.UserName),
                new Claim("UserType",user.UserType.ToString()),
            };

            //需要加密key
            //Nuget引入：Microsoft.IdentityModel.Tokens
            
            //_JWTTokenOptions.SecurityKey 属性存储了字符串形式的 JWT 安全密钥，
            //而 key 是将该安全密钥转换为 SymmetricSecurityKey 对象，以便在 JWT 的签名凭证中使用。
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));

            //使用刚才创建的密钥和 SecurityAlgorithms.HmacSha256 算法进行签名凭证的设置。
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Nuget引入：System.IdentityModel.Tokens.Jwt
            //创建了一个 JWT 令牌对象，设置了签发者、受众、有效载荷、过期时间和签名凭证等参数。
            JwtSecurityToken token = new JwtSecurityToken(
             issuer: _JWTTokenOptions.Issuer, // 签发者
             audience: _JWTTokenOptions.Audience, // 受众
             claims: claims, // 有效载荷
             expires: DateTime.Now.AddMinutes(10),//5分钟有效期
             signingCredentials: creds); //签名凭证

            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            //最后，使用 JwtSecurityTokenHandler 的 WriteToken 方法将 JWT 令牌转换为字符串形式，并将其作为方法的返回值。
            return returnToken;
            #endregion
        }
    }
}
