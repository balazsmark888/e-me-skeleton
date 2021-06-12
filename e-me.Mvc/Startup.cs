using System;
using e_me.Business.Services.Implementations;
using e_me.Business.Services.Interfaces;
using e_me.Core.Logging;
using e_me.Model;
using e_me.Model.DBContext;
using e_me.Mvc.Auth;
using e_me.Mvc.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace e_me.Mvc
{
    /// <summary>
    /// Startup class of the MVC project.
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configures the services to be injected.
        /// </summary>
        /// <param name="services">The collection of services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(1);
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IEncryptionProviderFactory, EncryptionProviderFactory>();
            services.AddTransient(sp => sp.GetRequiredService<IEncryptionProviderFactory>().Create());

            services.AddTransient<IApplicationUserContextFactory, ApplicationUserContextFactory>();
            services.AddTransient(sp => sp.GetRequiredService<IApplicationUserContextFactory>().Create());

            services.AddSingleton(ApplicationLoggerFactory.Create(Configuration));

            services.AddTransient<IApplicationDbContextFactory, ApplicationDbContextFactory>();
            services.AddTransient(sp => sp.GetRequiredService<IApplicationDbContextFactory>().Create());

            services.AddRepositories();

            services.AddTransient<Configuration>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDocumentService, DocumentService>();

            services.AddJwtBearerAuthentication(Configuration);

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "E-me API";
                    document.Info.Description = ".NET Core Web API for my document handling project.";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Balazs Mark",
                        Email = "balazsmark888@gmail.com",
                    };
                };
            });
        }

        /// <summary>
        /// Configures the application pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseJwtTokenValidator(new JwtTokenValidatorOptions());
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "defaultApi",
                    pattern: "api/{controller}/{id?}");
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
