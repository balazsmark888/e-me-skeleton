using System;
using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.ViewModels;
using GoogleVisionBarCodeScanner;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ContentPage
    {
        private readonly INavigationService _navigationService;
        private readonly DocumentsViewModel _documentsViewModel;
        private readonly ApplicationContext _applicationContext;

        public ScannerPage(INavigationService navigationService,
            DocumentsViewModel documentsViewModel,
            ApplicationContext applicationContext)
        {
            _navigationService = navigationService;
            _documentsViewModel = documentsViewModel;
            _applicationContext = applicationContext;
            Methods.SetSupportBarcodeFormat(BarcodeFormats.QRCode);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Shell.SetTabBarIsVisible(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            _navigationService.NavigateTo<AppShell>();
            return true;
        }

        private async void CameraView_OnDetected(object sender, GoogleVisionBarCodeScanner.OnDetectedEventArg e)
        {
            var obj = e.BarcodeResults;

            var result = obj[0].DisplayValue;
            if (result == null) return;
            try
            {
                var document = _documentsViewModel.GetDocumentFromCode(result);
                _applicationContext.ApplicationSecureStorage[Constants.CurrentDocumentProperty] = document;
                _navigationService.NavigateTo<DocumentPage>();
            }
            catch (Exception exception)
            {
                DisplayAlert("Error", "Invalid code!", "OK");
            }
        }
    }
}