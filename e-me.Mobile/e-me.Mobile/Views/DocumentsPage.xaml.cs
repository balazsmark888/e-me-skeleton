using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.ViewModels;
using e_me.Shared.DTOs.Document;
using Telerik.XamarinForms.DataControls.ListView;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentsPage : ContentPage
    {
        private readonly DocumentsViewModel _documentsViewModel;
        private readonly INavigationService _navigationService;
        private readonly ApplicationContext _applicationContext;

        public DocumentsPage(DocumentsViewModel documentsViewModel,
            INavigationService navigationService,
            ApplicationContext applicationContext)
        {
            _documentsViewModel = documentsViewModel;
            _navigationService = navigationService;
            _applicationContext = applicationContext;
            InitializeComponent();
            BindingContext = _documentsViewModel;

            DocumentsListView.ItemTemplate = new DataTemplate(() =>
            {
                var row = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Spacing = 0,
                };
                var label = new Label { WidthRequest = 240, HorizontalTextAlignment = TextAlignment.Start, Margin = new Thickness(10), FontSize = 18, HorizontalOptions = LayoutOptions.Start, TextColor = Color.Black };
                var deleteButton = new RadButton
                {
                    HorizontalOptions = LayoutOptions.End,
                    HeightRequest = 25,
                    WidthRequest = 60,
                    BackgroundColor = Color.IndianRed,
                    Text = "Remove",
                    TextColor = Color.White,
                    FontSize = 10,
                    Margin = new Thickness(5, 5, 5, 5),
                };
                var shareButton = new RadButton
                {
                    HorizontalOptions = LayoutOptions.End,
                    HeightRequest = 25,
                    WidthRequest = 60,
                    BackgroundColor = System.Drawing.Color.CornflowerBlue,
                    Text = "Share",
                    TextColor = Color.White,
                    FontSize = 10,
                    Margin = new Thickness(0, 5, 0, 5),
                };
                deleteButton.Clicked += DeleteButton_OnClicked;
                shareButton.Clicked += ShareButton_OnClicked;
                row.Children.Add(label);
                row.Children.Add(shareButton);
                row.Children.Add(deleteButton);
                label.SetBinding(Label.TextProperty, new Binding(nameof(DocumentTemplateListItemDto.DisplayName)));
                deleteButton.SetBinding(RadButton.CommandParameterProperty, new Binding(nameof(DocumentTemplateListItemDto.DocumentTemplateId)));
                shareButton.SetBinding(RadButton.CommandParameterProperty, new Binding(nameof(DocumentTemplateListItemDto.DocumentTemplateId)));
                return new ListViewTemplateCell
                {
                    View = row,
                };
            });
        }

        private void DeleteButton_OnClicked(object sender, EventArgs e)
        {
            if (!(sender is RadButton button)) return;
            var guid = (Guid)button.CommandParameter;
            _documentsViewModel.DeleteDocument(guid);
            DocumentsListView.ItemsSource = _documentsViewModel.OwnedDocumentTypes;
        }

        private void ShareButton_OnClicked(object sender, EventArgs e)
        {
            if (!(sender is RadButton button)) return;
            var guid = (Guid)button.CommandParameter;
            _applicationContext.ApplicationSecureStorage[Constants.ShareDocumentProperty] = guid;
            _navigationService.NavigateTo<ShareDocumentPage>();
        }

        protected override void OnAppearing()
        {
            Shell.SetTabBarIsVisible(this, true);
            DocumentsListView.ItemsSource = _documentsViewModel.OwnedDocumentTypes;
        }

        private void DocumentsListView_OnSelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null) return;
            var document = _documentsViewModel.OnItemSelected((DocumentTemplateListItemDto)e.NewItems[0]);
            _applicationContext.ApplicationSecureStorage[Constants.CurrentDocumentProperty] = document;
            _navigationService.NavigateTo<DocumentPage>();
        }

        private async void ScanDocument(object sender, EventArgs e)
        {
            try
            {
                var allowed = await GoogleVisionBarCodeScanner.Methods.AskForRequiredPermission();
                if (allowed)
                {
                    _navigationService.NavigateTo<ScannerPage>();
                }
            }
            catch (Exception)
            {
                //ignored
            }
        }
    }
}