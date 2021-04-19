using System;
using System.Net.Http;
using e_me.Mobile.AppContext;
using e_me.Mobile.MenuItems;
using e_me.Mobile.Services.Crypto;
using e_me.Mobile.Services.DataStores;
using e_me.Mobile.Services.HttpClientService;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.Services.User;
using e_me.Mobile.ViewModels;
using e_me.Mobile.Views;
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
            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    c.AddCommandLine(new[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
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
            services.AddSingleton<App>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IApplicationContextFactory, ApplicationContextFactory>();
            services.AddTransient(sp => sp.GetRequiredService<IApplicationContextFactory>().CreateApplicationContext());
            services.AddTransient<INavigationService, NavigationService>();
            services.AddTransient<IHttpClientFactory, ApplicationHttpClientFactory>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICryptoService, ApplicationCryptoService>();
            services.AddTransient<DocumentTypeDataStore>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<DocumentTypesViewModel>();

            services.AddTransient<MainPage>();
            services.AddTransient<RegisterPage>();
            services.AddTransient<LoginPage>();
            services.AddTransient<DocumentsPage>();
            services.AddTransient<DocumentTypesPage>();

            services.AddTransient<DocumentsFlyoutItem>();
            services.AddTransient<DocumentTypesFlyoutItem>();
            services.AddTransient<MainFlyoutItem>();
            services.AddTransient<LoginFlyoutItem>();
            services.AddTransient<RegisterFlyoutItem>();

            services.AddSingleton<AppShell>();

        }
    }
}
