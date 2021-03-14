using System;
using e_me.Business;
using e_me.Business.Interfaces;
using e_me.Core.Application;
using e_me.Core.Logging;
using e_me.Model;
using e_me.Model.DBContext;
using e_me.Mvc.Application;
using e_me.Mvc.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace e_me.Mvc
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IApplicationUserContextFactory, ApplicationUserContextFactory>();
            services.AddTransient(sp => sp.GetRequiredService<IApplicationUserContextFactory>().Create());

            services.AddSingleton(ApplicationLoggerFactory.Create(Configuration));

            services.AddTransient<IApplicationDbContextFactory, ApplicationDbContextFactory>();
            services.AddTransient(sp => sp.GetRequiredService<IApplicationDbContextFactory>().Create());

            services.AddRepositories();
			services.AddTransient<Configuration>();

            services.AddTransient<IUserService, UserService>();

            services.AddJwtBearerAuthentication(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "defaultApi",
                    pattern: "api/{controller}/{id?}");
            });

            app.UseWebSockets();

            app.Use(async (httpContext, next) =>
            {
                if (httpContext.WebSockets.IsWebSocketRequest && httpContext.Request.Path.StartsWithSegments("/wss"))
                {
                    /*TODO: check if ECDF handshake*/
                }
                else
                {
                    await next();
                }
            });

        }
    }
}
