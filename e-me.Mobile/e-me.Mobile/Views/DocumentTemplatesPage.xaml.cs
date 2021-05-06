using System.Collections.Specialized;
using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.ViewModels;
using e_me.Shared.DTOs.Document;
using Telerik.XamarinForms.DataControls.ListView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentTemplatesPage : ContentPage
    {
        private readonly DocumentTemplatesViewModel _documentTemplatesViewModel;
        private readonly INavigationService _navigationService;
        private readonly ApplicationContext _applicationContext;

        public DocumentTemplatesPage(DocumentTemplatesViewModel documentTemplatesViewModel, INavigationService navigationService,ApplicationContext applicationContext)
        {
            _documentTemplatesViewModel = documentTemplatesViewModel;
            _navigationService = navigationService;
            _applicationContext = applicationContext;
            InitializeComponent();
            BindingContext = _documentTemplatesViewModel;
            DocumentTemplatesListView.ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label { Margin = new Thickness(10) };
                var content = new Grid();
                content.Children.Add(label);
                label.SetBinding(Label.TextProperty, new Binding(nameof(DocumentTemplateListItemDto.DisplayName)));

                return new ListViewTemplateCell
                {
                    View = content
                };
            });
        }

        protected override void OnAppearing()
        {
            Shell.SetTabBarIsVisible(this, true);
            DocumentTemplatesListView.ItemsSource = _documentTemplatesViewModel.AvailableDocumentTypes;
        }

        private void DocumentTemplatesListView_OnSelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null) return;
            var document = _documentTemplatesViewModel.OnItemSelected((DocumentTemplateListItemDto)e.NewItems[0]);
            _applicationContext.ApplicationSecureStorage[Constants.CurrentDocumentProperty] = document;
            _navigationService.NavigateTo<DocumentPage>();
        }
    }
}