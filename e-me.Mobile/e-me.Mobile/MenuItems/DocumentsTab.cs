using e_me.Mobile.Views;
using Xamarin.Forms;

namespace e_me.Mobile.MenuItems
{
    public class DocumentsTab : Tab
    {
        public DocumentsTab(DocumentsPage documentsPage)
        {
            Items.Clear();
            Items.Add(new ShellContent
            {
                Content = documentsPage,
                Title = "Documents"
            });
            Title = "My Documents";
        }
    }
}
