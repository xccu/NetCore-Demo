using Services;
using SqlSugar;

//https://www.donet5.com/Home/Doc?typeId=1180

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var conn = configuration.GetConnectionString("MySql");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ע��SqlSugar��AddScoped
builder.Services.AddScoped<ISqlSugarClient>(s =>
{
    //Scoped��SqlSugarClient 
    SqlSugarClient sqlSugar = new SqlSugarClient(new ConnectionConfig()
    {
        DbType = SqlSugar.DbType.MySql,
        ConnectionString = conn,
        IsAutoCloseConnection = true,
    },
   db =>
   {
       //�����������ã�������������Ч
       db.Aop.OnLogExecuting = (sql, pars) =>
       {
           //��ȡIOC����Ҫ����һ��������
           //vra log=s.GetService<Log>()

           //��ȡIOC����Ҫ����һ��������
           //var appServive = s.GetService<IHttpContextAccessor>();
           //var log= appServive?.HttpContext?.RequestServices.GetService<Log>();
       };
   });
    return sqlSugar;
});

builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
