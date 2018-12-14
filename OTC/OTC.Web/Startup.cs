using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Targets;
using OTC.Data;
using OTC.Repository.Sys;
using Swashbuckle.AspNetCore.Swagger;

namespace OTC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //访问授权
            services.AddAuthentication("OTCScheme")
              .AddCookie("OTCScheme", options =>
              {
                  options.AccessDeniedPath = "/Account/Forbidden/";
                  options.LoginPath = "/Account/Unauthorized/";
                  options.Cookie.HttpOnly = true;
                  //options.DataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo(env.ContentRootPath));
                  //options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                  //options.Events = new CookieAuthenticationEvents
                  //{
                  //    OnValidatePrincipal = LastChangedValidator.ValidateAsync
                  //};
              });
            //上面的代码片段配置了以下部分或全部选项：

            //AccessDeniedPath - 当用户尝试访问资源但没有通过任何授权策略时，这是请求会重定向的相对路径资源。
            //AuthenticationScheme - 这是一个已知的特定Cookie认证方案的值。当有多个Cookie验证实例，并且您想限制对一个实例的授权时，这就非常有用。
            //AutomaticAuthenticate - 此标识仅适用于ASP.NET Core 1.x。它表示Cookie身份验证应在每个请求上运行，并尝试验证和重建序列化主体。
            //AutomaticChallenge - 此标识仅适用于ASP.NET Core 1.x。这表示当授权失败时，1.x Cookie认证应将浏览器重定向到LoginPath或AccessDeniedPath。
            //LoginPath - 当用户尝试访问资源但尚未认证时，这是请求重定向的相对路径。
            //DataProtectionProvider - 因为这里是要做服务集群的，如果单机或单服务实例情况下，采用默认DataProtection机制就可以了。代码中手动指定目录创建，与默认实现的区别就是，默认实现会生成一个与当前机器及应用相关的key进行数据加解密，而手动指定目录创建provider，会在指定的目录下生成一个key的xml文件。这样，服务集群部署时候，加解密key一样，加解密得到的报文也是一致的。
            //Events 一旦创建了认证的Cookie，它将成为唯一的身份来源。即使您在服务系统中禁用用户，Cookie身份验证也无法了解此信息，只要Cookie有效，用户仍可登录。 Cookie认证在其选项中提供了一系列事件,ValidateAsync()事件可用于拦截和重写Cookie身份验证。可以考虑在后端用户数据库中增加LastChanged列。为了在数据库更改时使Cookie无效，您应该首先在创建Cookie时添加一个LastChanged包含当前值的声明。数据库更改时，更新LastChanged例的值。

            //配置swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "OTC API"
                });

                //Determine base path for the application.  
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                //Set the comments path for the swagger json and ui.  
                var xmlPath = Path.Combine(basePath, "OTC.Web.xml");
                options.IncludeXmlComments(xmlPath);
                //options.OperationFilter<HttpHeaderFilter>();
            });

            ////配置EF的服务注册
            //services.AddEntityFrameworkMySql()
            //    .AddDbContext<DataContext>(options =>
            //    {
            //        options.UseMySql(Configuration.GetConnectionString("test"));//读取配置文件中的链接字符串
            //    });
            DataContext.ConStr = Configuration.GetConnectionString("test");
            services.AddTransient<ILogRepository, LogRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //解决日期格式带T的问题
            //.AddJsonOptions(option=>option.SerializerSettings.DateFormatString= "yyyy-MM-dd HH:mm:ss")
            //JSON首字母小写解决
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());
            //无论是EF 6.x还是EF Core 2.1中的延迟加载都无法避免循环引用问题，若在.NET Core Web应用程序中启用了延迟加载，我们需要在ConfigureServices方法中进行如下配置才行。
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            //Logger
            services.AddLogging(builder =>
            {
                builder
                    .AddConfiguration(Configuration.GetSection("Logging"))
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddConsole();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //env.EnvironmentName = EnvironmentName.Production;
            //为了应用程序的安全，一般不会在生产环境中启起用开发者页面（异常页面）。
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseAuthentication();
            //添加NLog
            loggerFactory.AddNLog();
            NLog.LogManager.LoadConfiguration($"{env.ContentRootPath}/nlog.config");
            NLog.LogManager.Configuration.FindTargetByName<DatabaseTarget>("database").ConnectionString = Configuration.GetConnectionString("test");
            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MySystem API V1");
            });

            app.UseMvc();
        }
    }
}
