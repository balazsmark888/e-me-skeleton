using e_me.Mobile.Views;
using Xamarin.Forms;

namespace e_me.Mobile.MenuItems
{
    public class DocumentTypesFlyoutItem : FlyoutItem
    {
        public DocumentTypesFlyoutItem(DocumentTypesPage documentTypesPage)
        {
            Items.Clear();
            Items.Add(new ShellContent
            {
                Content = documentTypesPage,
                Title = "Document Types"
            });
            Title = "Document Types";
        }
    }
}
