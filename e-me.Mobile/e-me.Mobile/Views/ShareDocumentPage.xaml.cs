using System;
using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.ViewModels;
using Telerik.XamarinForms.Barcode;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShareDocumentPage : ContentPage
    {
        private readonly INavigationService _navigationService;
        private readonly DocumentsViewModel _documentsViewModel;
        private readonly ApplicationContext _applicationContext;

        public ShareDocumentPage(INavigationService navigationService,
            DocumentsViewModel documentsViewModel,
            ApplicationContext applicationContext)
        {
            _navigationService = navigationService;
            _documentsViewModel = documentsViewModel;
            _applicationContext = applicationContext;
            InitializeComponent();
            SetToken();
        }

        public void SetToken()
        {
            var templateId = _applicationContext.ApplicationSecureStorage[Constants.ShareDocumentProperty] as Guid?;
            if (templateId != null)
            {
                var barCode = new RadBarcode
                {
                    WidthRequest = 250,
                    HeightRequest = 250,
                    Symbology = new QRCode
                    {
                        SizingMode = SizingMode.Stretch
                    }
                };
                var label = new Label
                {
                    Text = "Scan this code to view the document",
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                var layout = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                barCode.Value = _documentsViewModel.GetAccessToken(templateId.Value);
                layout.Children.Add(label);
                layout.Children.Add(barCode);
                QrFrame.Content = layout;
            }
        }

        protected override void OnAppearing()
        {
            Shell.SetNavBarIsVisible(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            _navigationService.NavigateTo<AppShell>();
            return true;
        }
    }
}