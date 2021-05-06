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
                Title = "My Documents"
            });
            Title = "My Documents";
        }
    }
}
