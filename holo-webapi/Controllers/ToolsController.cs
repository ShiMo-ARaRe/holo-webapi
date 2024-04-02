using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using holo_webapi.Common;

namespace holo_webapi.WebAPI.Controllers
{
    /// <summary>
    /// 创建数据库，并创建表，并插入数据
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        [HttpGet]
        public void InitDatabase()
        {
            DbContext.InitDataBase();
        }
    }
}
