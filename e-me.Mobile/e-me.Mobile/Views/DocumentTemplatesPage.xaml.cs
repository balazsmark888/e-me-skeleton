using System.Collections.Specialized;
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

        public DocumentTemplatesPage(DocumentTemplatesViewModel documentTemplatesViewModel)
        {
            _documentTemplatesViewModel = documentTemplatesViewModel;
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
            DocumentTemplatesListView.ItemsSource = _documentTemplatesViewModel.DocumentTypes;
        }

        private void DocumentTemplatesListView_OnSelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var document = _documentTemplatesViewModel.OnItemSelected((DocumentTemplateListItemDto)e.NewItems[0]);
            Application.Current.MainPage = new DocumentPage(document);
        }
    }
}