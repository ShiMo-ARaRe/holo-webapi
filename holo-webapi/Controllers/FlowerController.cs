using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using holo_webapi.Model;
using holo_webapi.Service;
using holo_webapi.Service.Flower;
using holo_webapi.Service.Flower.Dto;

namespace holo_webapi.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FlowerController : ControllerBase
    {
        public IFlowerService _flowerService;
        public FlowerController(IFlowerService flowerService)
        {
            _flowerService = flowerService;
        }
        [HttpPost]
        public ApiResult GetFlowers(FlowerReq req)
        {
            ApiResult apiResult = new ApiResult() { IsSuccess = true }; // 表示调用成功
            apiResult.Result = _flowerService.GetFlowers(req);
            return apiResult;
        }
    }
}
