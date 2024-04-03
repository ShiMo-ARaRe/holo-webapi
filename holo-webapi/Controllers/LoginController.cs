using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using holo_webapi.Common;
using holo_webapi.Model;
using holo_webapi.Service;
using holo_webapi.Service.User.Dto;
using holo_webapi.Service.User;
using holo_webapi.Service.Jwt;

namespace holo_webapi.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IUserService _userService; // 用户层相关服务
        public ICustomJWTService _customJWTService; // 用于生成token
        public LoginController(IUserService userService, ICustomJWTService customJWTService)
        {
            _userService = userService;
            _customJWTService = customJWTService;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetValidateCodeImages(string t) // 根据前端逻辑， ValidateKey 和 t 来自同一个guid
        /*  IActionResult 是ASP.NET Core中用于表示控制器方法返回结果的接口。
            它是一个通用的接口，可以表示各种不同类型的结果，例如视图、重定向、文件等。
            它提供了一种统一的方式来处理和返回不同类型的结果。*/
        {
            var validateCodeString = Tools.CreateValidateString(); // 生成随机验证码
            //将验证码记入缓存中
            MemoryHelper.SetMemory(t, validateCodeString, 5); // guid为键，验证码为要存储的对象，过期时间为5分种
            //接收图片返回的二进制流
            byte[] buffer = Tools.CreateValidateCodeBuffer(validateCodeString); // 根据验证码来生成图片
            return File(buffer, @"image/jpeg"); // 并返回给前端
            /*  File(buffer, @"image/jpeg") 是 IActionResult 接口的一种实现，用于返回一个文件结果。
                它接收两个参数，第一个参数 buffer 是要返回的文件内容的字节数组，
                第二个参数 @"image/jpeg" 是文件的 MIME 类型，指示返回的文件是 JPEG 图片。*/
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Check(UserReq req)
        {
            var currCode = MemoryHelper.GetMemory(req.ValidateKey); // 通过key，从缓存中获取验证码
            ApiResult apiResult = new ApiResult() { IsSuccess = false }; // 默认失败
            if (string.IsNullOrEmpty(req.UserName) || string.IsNullOrEmpty(req.Password) || string.IsNullOrEmpty(req.ValidateKey) || string.IsNullOrEmpty(req.ValidateCode)) // 如果...为 null 或空字符串
            {
                apiResult.Msg = "参数不能为空！";
            }
            else if (currCode == null)
            {
                apiResult.Msg = "验证码不存在，请刷新验证码！";
            }
            else if (currCode.ToString() != req.ValidateCode)
            {
                apiResult.Msg = "验证码错误，请重新输入或刷新重试！";
            }
            else
            {
                UserRes user = _userService.GetUsers(req); // 从 请求数据传输对象==>响应数据传输对象
                if (string.IsNullOrEmpty(user.UserName)) // 如果user.UserName为 null 或空字符串
                {
                    apiResult.Msg = "账号不存在，用户名或密码错误！";
                }
                else
                {
                    apiResult.IsSuccess = true; // 成功
                    apiResult.Result = _customJWTService.GetToken(user); // 只需返回token，因为用户信息包含在token当中
                }
            }
            return apiResult;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Register(RegisterReq req)
        {
            var currCode = MemoryHelper.GetMemory(req.ValidateKey); // 通过key，从缓存中获取验证码
            ApiResult apiResult = new ApiResult() { IsSuccess = false }; // 默认失败
            if (string.IsNullOrEmpty(req.UserName) || string.IsNullOrEmpty(req.Password) || string.IsNullOrEmpty(req.NickName) || string.IsNullOrEmpty(req.ValidateKey) || string.IsNullOrEmpty(req.ValidateCode)) // 如果...为 null 或空字符串
            {
                apiResult.Msg = "参数不能为空！";
            }
            else if (currCode == null)
            {
                apiResult.Msg = "验证码不存在，请刷新验证码！";
            }
            else if (currCode.ToString() != req.ValidateCode)
            {
                apiResult.Msg = "验证码错误，请重新输入或刷新重试！";
            }
            else
            {
                string msg = string.Empty; //初始化为空字符串
                var res = _userService.RegisterUser(req, ref msg); // 从 RegisterReq ==> Users ,再从 Users ==> UserRes
                if (!string.IsNullOrEmpty(msg)) //如果 msg 不为空，则执行
                {
                    apiResult.Msg = msg;
                }
                else
                {
                    apiResult.IsSuccess = true;
                    apiResult.Result = _customJWTService.GetToken(res);
                }
            }
            return apiResult;
        }
    }
}
