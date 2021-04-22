using e_me.Mobile.Views;
using Xamarin.Forms;

namespace e_me.Mobile.MenuItems
{
    public class DocumentTypesTab : Tab
    {
        public DocumentTypesTab(DocumentTemplatesPage documentTemplatesPage)
        {
            Items.Clear();
            Items.Add(new ShellContent
            {
                Content = documentTemplatesPage,
                Title = "Document Types"
            });
            Title = "Document Types";
        }
    }
}
