var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Middleware1 start\n");
    await next.Invoke();
    await context.Response.WriteAsync("Middleware1 end\n");
});
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Middleware2 start\n");
    await next.Invoke();
    await context.Response.WriteAsync("Middleware2 end\n");
});
//app.Use(_ =>
//{
//    return context =>
//    {
//        return context.Response.WriteAsync("Middleware3 start\n");
//    };
//});

//Run ί�в����յ� next ������ ��һ�� Run ί��ʼ��Ϊ�նˣ�������ֹ�ܵ��� Run ��һ��Լ���� 
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello World!\n");
});


app.Run();

#region old
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
#endregion