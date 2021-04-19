using e_me.Mobile.Models;
using e_me.Mobile.ViewModels;
using Telerik.XamarinForms.DataControls;
using Telerik.XamarinForms.DataControls.ListView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentTypesPage : ContentPage
    {
        public DocumentTypesPage(DocumentTypesViewModel documentTypesViewModel)
        {
            InitializeComponent();
            DocumentTypesListView = new RadListView
            {
                ItemsSource = documentTypesViewModel.DocumentTypes,
                ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label {Margin = new Thickness(10)};
                    var content = new Grid();
                    content.Children.Add(label);
                    label.SetBinding(Label.TextProperty, new Binding(nameof(DocumentType.DisplayName)));

                    return new ListViewTemplateCell
                    {
                        View = content
                    };
                })
            };
        }
    }
}