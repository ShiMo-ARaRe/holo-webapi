/*  �ֲ�ܹ�
holo_webapi         ��ʾ��(�����û����棨UI���Ϳ�������Controller��)
holo_webapi.Service �����
holo_webapi.Common  ���߲�/������/���ݿ���ʲ�
holo_webapi.Model   ģ�Ͳ�
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
using System.Text.Json; // Ĭ�ϵ�JSON���л���
/*  ����һ�����ܸ�Ϊǿ���JSON���л��⣬֧�ָ�������Ժ�ѡ�
    ��Ҫ��װNewtonsoft.Json����Microsoft.AspNetCore.Mvc.NewtonsoftJson�� */
using Newtonsoft.Json;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using holo_webapi.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    //����JSON�������ڸ�ʽ
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    #region ����չʾע��
    {
        /* xml�ĵ�����·�� 
        AppContext.BaseDirectory��ִ��Ŀ¼/��Ŀ¼ */
        var file = Path.Combine(AppContext.BaseDirectory, "holo_webapi.xml");
        // file �� XML ע���ļ���·����true ��ʾҪ���ÿ��������ע����ʾ��
        option.IncludeXmlComments(file, true);
        // ��action�����ƽ�����������ж�����Ϳ��Կ���Ч���ˡ�
        option.OrderActionsBy(o => o.RelativePath);
        /* ���������ʹ�� o => o.RelativePath �� lambda ���ʽ��Ϊ��������ʾ���ն��������·����������
          * ���������� Swagger �ĵ��а���·����˳����ʾ API ������ʹ�������������ڲ��ҡ�*/
    }
    #endregion
});

//��ӿ������
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-Pagination"));
});

//ע����־
builder.Logging.AddLog4Net("Config/log4net.Config"); // ����Ļ��Ͱ�װMicrosoft.Extensions.Logging.Log4Net.AspNetCore��

/* ע��Automapper
�� AutoMapperConfigs ����ע�ᵽ DI �����У��Ա���Ӧ�ó�����ʹ�� AutoMapper ���ж���ӳ�䡣
�����Ϳ����������ط�ͨ�� DI ��������ȡ AutoMapper ��ʵ������ʹ�ö���õ�ӳ���ϵ���ж����ת����*/
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));

//ע��JWT
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions")); // ע�ᶼ�����ˣ�

#region ����ע��

//����һ��ֱ��ע��Service�����
//builder.Services.AddTransient<IFlowerService, FlowerService>();
//builder.Services.AddTransient<IUserService, UserService>();
//builder.Services.AddTransient<IOrderService, OrderService>();
//builder.Services.AddTransient<ICustomJWTService, CustomJWTService>();

//�������� Autofacע��
builder.Host    // builder.Host��һ��WebHostBuilder��������ԣ���ʾ���ڹ�����Web����ʵ����
.UseServiceProviderFactory(new AutofacServiceProviderFactory())
/* ��Autofac��AutofacServiceProviderFactory����ΪWebHostBuilder�ķ����ṩ���򹤳���
   ����ζ���ڹ���Web����ʱ����ʹ��Autofac��Ϊ����ע��������*/
.ConfigureContainer<ContainerBuilder>(builder =>
{/*
    ͨ��.ConfigureContainer<ContainerBuilder>(builder => { ... })�������������������á�
    ����ʹ���˷������أ�ָ����ContainerBuilder��Ϊ�������������͡�

    ��������ù����У�������builder.RegisterModule(new AutofacModuleRegister())������
    ��AutofacModuleRegisterע��ΪAutofac��ģ�顣��������Ϊ�˽�AutofacModuleRegister�ж����������ע���߼�Ӧ�õ�Autofac�����С�*/
    builder.RegisterModule(new AutofacModuleRegister());
});
#endregion

#region jwtУ�� 
{
    //�ڶ��������Ӽ�Ȩ�߼�
    JWTTokenOptions tokenOptions = new JWTTokenOptions(); // ����ʵ��
    // �������ļ��е� "JWTTokenOptions" �ڵ��ֵ�󶨵� tokenOptions ����
    builder.Configuration.Bind("JWTTokenOptions", tokenOptions);

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
     /* ����������Ϊʹ�� JWT Bearer �����֤������JwtBearerDefaults.AuthenticationScheme����
      * ��������ʹ�� .AddJwtBearer ���������� JWT Bearer �����֤��ѡ����߼���*/
     .AddJwtBearer(options =>  //���������õļ�Ȩ���߼�
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             //JWT��һЩĬ�ϵ����ԣ����Ǹ���Ȩʱ�Ϳ���ɸѡ��
             ValidateIssuer = true,//�Ƿ���֤Issuer��ǩ���ߣ�
             ValidateAudience = true,//�Ƿ���֤Audience�����ڣ�
             ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
             ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey ��������Կ��
             ValidAudience = tokenOptions.Audience,//ָ����Ч�� Audience ֵ�������� JWT �е� Audience ���бȽ���֤��
             ValidIssuer = tokenOptions.Issuer,    //ָ����Ч�� Issuer ֵ�������� JWT �е� Issuer ���бȽ���֤��
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))//�õ�SecurityKey ��������Կ��
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
app.UseSwaggerUI(); // ����ģʽҲ�򿪣��������

#region ��Ȩ��Ȩ
app.UseAuthentication(); //��Ȩ    ----��������ʱ�򣬰������д���token/Session/Cookies��������ȡ���û���Ϣ
app.UseAuthorization();  //��Ȩ    --- �Ѿ��õ����û���Ϣ���Ϳ���ͨ���û���Ϣ���ж���ǰ�û��Ƿ���Է��ʵ�ǰ��Դ
#endregion

app.MapControllers();

//ʹ�ÿ������
app.UseCors("CorsPolicy");

app.Run();
