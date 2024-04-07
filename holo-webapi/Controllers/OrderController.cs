using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using holo_webapi.Model;
using holo_webapi.Service;
using holo_webapi.Service.Order;
using holo_webapi.Service.Order.Dto;

namespace holo_webapi.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize] // 表示给控制器启用鉴权，那么控制器下的所有方法都需要通过鉴权
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger; // 记录日志
        private IOrderService _orderService;
        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult CreateOrder(OrderReq req) //请求数据传输对象，只有 商品ID 这一个属性
        {
            ApiResult apiResult = new ApiResult() { IsSuccess = false }; // 默认失败
            if (req.FlowerId == 0)
            {
                apiResult.Msg = "参数不可以为空！";
            }
            else
            {
                string msg = string.Empty; // 默认为空
                long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value); // 从token中获取用户信息
                bool res = _orderService.CreateOrder(req, userId, ref msg);  // 创建订单
                if (!string.IsNullOrEmpty(msg)) // 错误信息不为空则执行
                {
                    apiResult.Msg = msg;
                }
                else
                {
                    apiResult.IsSuccess = res; // res是bool类型，该路由方法不需要Result
                }
            }
            return apiResult;
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult GetOrder() // 请求头中携带了token，所以不需要传其他参数，不然的话不安全。
        {
            ApiResult apiResult = new ApiResult() { IsSuccess = true }; // 默认成功
            try
            {
                long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value); // 从token中获取用户信息
                apiResult.Result = _orderService.GetOrder(userId); // 用于给前端展示
                _logger.LogInformation("this is GetOrder。。。");  // 信息级别的日志消息
            }
            catch (Exception ex)
            {
                apiResult.IsSuccess = false; //失败
                apiResult.Msg = ex.Message;  // 错误信息
            }
            return apiResult;
        }

    }
}
