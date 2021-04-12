using System;
using System.IO;
using e_me.Mobile.AppContext;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.ViewModels;
using e_me.Mobile.Views;
using e_me.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xamarin.Essentials;

namespace e_me.Mobile
{
    public class Startup
    {
        public static App Init()
        {
            var systemDir = FileSystem.CacheDirectory;
            Utils.ExtractSaveResource("e_me.Mobile.appsettings.json", systemDir);
            var fullConfig = Path.Combine(systemDir, "e_me.Mobile.appsettings.json");

            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    c.AddCommandLine(new[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    c.AddJsonFile(fullConfig);
                })
                .ConfigureServices(ConfigureServices)
                .Build();
            App.ServiceProvider = host.Services;
            return App.ServiceProvider.GetService<App>();
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            if (ctx.HostingEnvironment.IsDevelopment())
            {
            }

            services.AddHttpClient("E-meApi", client =>
            {
                client.BaseAddress = new Uri(ctx.Configuration["BackendUrl"]);

            });
            services.AddSingleton<App>();
            services.AddTransient<IApplicationContextFactory, ApplicationContextFactory>();
            services.AddTransient(sp => sp.GetRequiredService<IApplicationContextFactory>().CreateApplicationContext());
            services.AddTransient<INavigationService, NavigationService>();

            services.AddTransient<IMainViewModel, MainViewModel>();
            services.AddTransient<MainPage>();
            services.AddTransient<IRegisterViewModel, RegisterViewModel>();
            services.AddTransient<RegisterPage>();
        }
    }


}
