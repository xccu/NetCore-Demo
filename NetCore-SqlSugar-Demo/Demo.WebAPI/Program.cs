using Services;
using SqlSugar;

//https://www.donet5.com/Home/Doc?typeId=1180

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var conn = configuration.GetConnectionString("SqlServer");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//https://www.donet5.com/Home/Doc?typeId=2247
//Register SqlSugar
builder.Services.AddScoped<ISqlSugarClient>(s =>
{
    SqlSugarClient sqlSugar = new SqlSugarClient(new ConnectionConfig()
    {
        DbType = SqlSugar.DbType.SqlServer,
        ConnectionString = conn,
        IsAutoCloseConnection = true,
    },
   db =>
   {
       db.Aop.OnLogExecuting = (sql, pars) =>
       {
           //vra log=s.GetService<Log>()

           //var appServive = s.GetService<IHttpContextAccessor>();
           //var log= appServive?.HttpContext?.RequestServices.GetService<Log>();
       };
   });
    return sqlSugar;
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ULockService>();

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
