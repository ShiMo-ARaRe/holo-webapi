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
using holo_webapi.Service.Order;
using holo_webapi.Service.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json; // 默认的JSON序列化库
/*  这是一个功能更为强大的JSON序列化库，支持更多的特性和选项。
    需要安装Newtonsoft.Json包和Microsoft.AspNetCore.Mvc.NewtonsoftJson包 */
using Newtonsoft.Json;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using holo_webapi.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    //设置JSON返回日期格式
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    #region 配置展示注释
    {
        /* xml文档绝对路径 
        AppContext.BaseDirectory是执行目录/根目录 */
        var file = Path.Combine(AppContext.BaseDirectory, "holo_webapi.xml");
        // file 是 XML 注释文件的路径，true 表示要启用控制器层的注释显示。
        option.IncludeXmlComments(file, true);
        // 对action的名称进行排序，如果有多个，就可以看见效果了。
        option.OrderActionsBy(o => o.RelativePath);
        /* 在这里，我们使用 o => o.RelativePath 的 lambda 表达式作为参数，表示按照动作的相对路径进行排序。
          * 这样可以在 Swagger 文档中按照路径的顺序显示 API 动作，使其更加有序和易于查找。*/
    }
    #endregion
});

//添加跨域策略
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-Pagination"));
});

//注册日志
builder.Logging.AddLog4Net("Config/log4net.Config"); // 报错的话就安装Microsoft.Extensions.Logging.Log4Net.AspNetCore包

/* 注册Automapper
将 AutoMapperConfigs 类型注册到 DI 容器中，以便在应用程序中使用 AutoMapper 进行对象映射。
这样就可以在其他地方通过 DI 容器来获取 AutoMapper 的实例，并使用定义好的映射关系进行对象的转换。*/
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));

//注册JWT
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions")); // 注册都别忘了！

#region 依赖注入

//方法一：直接注册Service层服务
//builder.Services.AddTransient<IFlowerService, FlowerService>();
//builder.Services.AddTransient<IUserService, UserService>();
//builder.Services.AddTransient<IOrderService, OrderService>();
//builder.Services.AddTransient<ICustomJWTService, CustomJWTService>();

//方法二： Autofac注入
builder.Host    // builder.Host是一个WebHostBuilder对象的属性，表示正在构建的Web主机实例。
.UseServiceProviderFactory(new AutofacServiceProviderFactory())
/* 将Autofac的AutofacServiceProviderFactory设置为WebHostBuilder的服务提供程序工厂。
   这意味着在构建Web主机时，将使用Autofac作为依赖注入容器。*/
.ConfigureContainer<ContainerBuilder>(builder =>
{/*
    通过.ConfigureContainer<ContainerBuilder>(builder => { ... })方法，对容器进行配置。
    这里使用了泛型重载，指定了ContainerBuilder作为配置容器的类型。

    在这个配置过程中，调用了builder.RegisterModule(new AutofacModuleRegister())方法，
    将AutofacModuleRegister注册为Autofac的模块。这样做是为了将AutofacModuleRegister中定义的依赖项注册逻辑应用到Autofac容器中。*/
    builder.RegisterModule(new AutofacModuleRegister());
});
#endregion

#region jwt校验 
{
    //第二步，增加鉴权逻辑
    JWTTokenOptions tokenOptions = new JWTTokenOptions(); // 创建实例
    // 将配置文件中的 "JWTTokenOptions" 节点的值绑定到 tokenOptions 对象。
    builder.Configuration.Bind("JWTTokenOptions", tokenOptions);

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
     /* 并将其配置为使用 JWT Bearer 身份验证方案（JwtBearerDefaults.AuthenticationScheme）。
      * 接下来，使用 .AddJwtBearer 方法来配置 JWT Bearer 身份验证的选项和逻辑。*/
     .AddJwtBearer(options =>  //这里是配置的鉴权的逻辑
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             //JWT有一些默认的属性，就是给鉴权时就可以筛选了
             ValidateIssuer = true,//是否验证Issuer（签发者）
             ValidateAudience = true,//是否验证Audience（受众）
             ValidateLifetime = true,//是否验证失效时间
             ValidateIssuerSigningKey = true,//是否验证SecurityKey （加密密钥）
             ValidAudience = tokenOptions.Audience,//指定有效的 Audience 值，用于与 JWT 中的 Audience 进行比较验证。
             ValidIssuer = tokenOptions.Issuer,    //指定有效的 Issuer 值，用于与 JWT 中的 Issuer 进行比较验证。
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))//拿到SecurityKey （加密密钥）
         };
     });
}
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(); // 生成模式也打开，方便测试

#region 鉴权授权
app.UseAuthentication(); //鉴权    ----请求来的时候，把请求中带的token/Session/Cookies做解析，取出用户信息
app.UseAuthorization();  //授权    --- 已经得到了用户信息，就可以通过用户信息来判定当前用户是否可以访问当前资源
#endregion

app.MapControllers();

//使用跨域策略
app.UseCors("CorsPolicy");

app.Run();
