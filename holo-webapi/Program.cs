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
using holo_webapi.Service.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//��ӿ������
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-Pagination"));
});

/* ע��Automapper
�� AutoMapperConfigs ����ע�ᵽ DI �����У��Ա���Ӧ�ó�����ʹ�� AutoMapper ���ж���ӳ�䡣
�����Ϳ����������ط�ͨ�� DI ��������ȡ AutoMapper ��ʵ������ʹ�ö���õ�ӳ���ϵ���ж����ת����*/
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));

//ע��JWT
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions")); // ע�ᶼ�����ˣ�

//ע��Service�����
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

//ʹ�ÿ������
app.UseCors("CorsPolicy");

app.Run();
