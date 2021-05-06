using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
using e_me.Mobile.Services.Navigation;
using e_me.Shared;
using e_me.Shared.DTOs.Document;
using Syncfusion.SfPdfViewer.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentPage : ContentPage
    {
        private readonly INavigationService _navigationService;
        private readonly ApplicationContext _applicationContext;

        public DocumentPage(INavigationService navigationService, ApplicationContext applicationContext)
        {
            _navigationService = navigationService;
            _applicationContext = applicationContext;
            InitializeComponent();
            SetDocument();
        }

        public void SetDocument()
        {

            var userDocumentDto = _applicationContext.ApplicationSecureStorage[Constants.CurrentDocumentProperty] as UserDocumentDto;
            PdfViewer.InputFileStream = new MemoryStream(userDocumentDto.File.FromBase64String());
        }

        protected override bool OnBackButtonPressed()
        {
            _navigationService.NavigateTo<AppShell>();
            return true;
        }

        private void PdfViewer_OnDoubleTapped(object sender, TouchInteractionEventArgs e)
        {
            Task.Run(() =>
            {
                if (PdfViewer.ZoomPercentage == 100)
                {
                    PdfViewer.ZoomPercentage = 200;
                    return;
                }

                PdfViewer.ZoomPercentage = 100;
            });
        }
    }
}