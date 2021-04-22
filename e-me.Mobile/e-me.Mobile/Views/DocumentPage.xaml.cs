using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using e_me.Shared;
using e_me.Shared.DTOs.Document;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentPage : ContentPage
    {
        public DocumentPage(UserDocumentDto userDocumentDto)
        {
            InitializeComponent();

            Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
            {
                Stream stream = new MemoryStream(userDocumentDto.File.FromBase64String());
                return stream;
            });
            PdfViewer.Source = streamFunc;
        }
    }
}