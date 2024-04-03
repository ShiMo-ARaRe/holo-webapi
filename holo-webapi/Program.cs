/*  分层架构
holo_webapi         表示层(包括用户界面（UI）和控制器（Controller）)
holo_webapi.Service 服务层
holo_webapi.Common  工具层/辅助层/数据库访问层
holo_webapi.Model   模型层
*/

using holo_webapi.Model;
using holo_webapi.Service.Config;
using holo_webapi.Service.Flower;
using holo_webapi.Service.Jwt;
using holo_webapi.Service.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//添加跨域策略
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-Pagination"));
});

/* 注册Automapper
将 AutoMapperConfigs 类型注册到 DI 容器中，以便在应用程序中使用 AutoMapper 进行对象映射。
这样就可以在其他地方通过 DI 容器来获取 AutoMapper 的实例，并使用定义好的映射关系进行对象的转换。*/
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));

//注册JWT
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions")); // 注册都别忘了！

//注册Service层服务
builder.Services.AddTransient<IFlowerService, FlowerService>();
builder.Services.AddTransient<IUserService, UserService>();
//builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ICustomJWTService, CustomJWTService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//使用跨域策略
app.UseCors("CorsPolicy");

app.Run();
