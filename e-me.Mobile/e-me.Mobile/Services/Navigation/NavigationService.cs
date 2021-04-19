﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace e_me.Mobile.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task GoBackAsync()
        {
            if (Application.Current.MainPage is NavigationPage navPage)
            {
                return navPage.PopAsync();
            }
            return Task.CompletedTask;
        }

        public void NavigateTo<TPageModel>() where TPageModel : Page
        {
            var page = _serviceProvider.GetRequiredService<TPageModel>();
            Shell.Current.CurrentItem = Shell.Current.Items.FirstOrDefault(p => p.CurrentItem.CurrentItem.Content is TPageModel model);
            //Application.Current.MainPage = page;
        }
    }
}
