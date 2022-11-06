using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_MiddleWare_Demo
{
    public class Startup
    {

        //https://www.bbsmax.com/A/kjdw88ZOzN/
        //https://www.cnblogs.com/kenwoo/p/9275922.html
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            //Run 委托不会收到 next 参数。 第一个 Run 委托始终为终端，用于终止管道。 Run 是一种约定。 
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!\n");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        #region old version
        //public void Configure_old(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseHttpsRedirection();

        //    app.UseRouting();

        //    app.UseAuthorization();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });
        //}
        #endregion
    }
}
