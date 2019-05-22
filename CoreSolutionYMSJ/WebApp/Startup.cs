using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLib;
using Entities;
using Entities.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Filter;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // 运行时调用此方法。使用此方法向容器中添加服务
        public void ConfigureServices(IServiceCollection services)
        {
            //注入Session
            services.AddSession();
            //注入缓存
            services.AddMemoryCache();

            //配置跨域处理，允许所有来源：
            services.AddCors(options =>
            options.AddPolicy("mycos",
            p => p.AllowAnyOrigin())
            );

            //注入EFContext
            services.AddDbContext<EFContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("conn"), b => b.UseRowNumberForPaging()));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // 此lambda确定给定请求是否需要非必需cookie的用户同意。
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //这里注入了一个全局异常处理过滤器
            services.AddMvc(a => { a.Filters.Add(typeof(GlobalExceptionFilter)); }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //配置文件注入-把配置文件读取转换成实体AppSet
            services.Configure<AppSet>(Configuration.GetSection("AppSet"));

            //程序集反射注入所有Service，省的写几百个
            services.AddAssembly("Services"); 
        }

        //运行时调用此方法。使用此方法配置HTTP请求管道
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();//使用session
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors("mycos");//设置可以跨域请求
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
